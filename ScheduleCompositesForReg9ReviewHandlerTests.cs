using System;
using System.Data.Entity;
using System.Linq;
using Fortigent.Bruce.Logic.Common.Commands;
using Fortigent.Bruce.Logic.Common.Services;
using Fortigent.Bruce.Logic.Compliance.Commands.Save.Reg9ScheduleComposites;
using Fortigent.Domains.Objects;
using Fortigent.Domains.Objects.Compliance;
using Fortigent.Domains.Repository;
using Fortigent.TDDUtilities;
using NSubstitute;
using NUnit.Framework;

namespace Fortigent.Bruce.Tests.Compliance.CommandTests.Reg9ScheduleComposites
{
    [TestFixture]
    public class SchedulingReg9FailureTests : SchedulingReg9FailureHandlerContext
    {
        public SchedulingReg9FailureTests()
        {
            Scenario("Trying to schedule a Reg 9 for review should throw exception");
        }

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            EstablishContext();
            given_the_reg9_to_be_scheduled_exists();
        }

        [Test]
        public void then_the_new_reg9_to_be_scheduled_should_not_be_created_by_the_user()
        {
            Assert.That(Reg9CompositesToBeScheduled.CreatedByUserId, Is.EqualTo(0));
        }

        [Test]
        public void then_the_new_reg9_to_be_scheduled_should_not_be_should_not_be_now()
        {
            Assert.That(Reg9CompositesToBeScheduled.CreatedDate, Is.EqualTo(new DateTime()));
        }

        [Test]
        public void then_the_changes_should_not_be_saved()
        {
            AllocationContextDb.DidNotReceive().SaveChanges();
        }

    }

    [TestFixture]
    public class SchedulingReg9SuccessTests : SchedulingReg9SuccessHandlerContext
    {
        public SchedulingReg9SuccessTests()
        {
            Scenario("Scheduling a Reg 9 for review should save the record");
        }

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            EstablishContext();
            when_the_reg9_to_be_scheduled_is_saved();
        }

        [Test]
        public void then_a_new_reg_9_is_added()
        {
            Assert.That(Reg9ToBeReviewed.Count(), Is.EqualTo(1));
        }

        [Test]
        public void then_the_added_Reg9_should_have_this_composite_id()
        {
            Assert.That(Reg9ToBeReviewed.FirstOrDefault().CompositeId, Is.EqualTo(12345));
        }

        [Test]
        public void then_the_added_Reg9_should_have_this_branch_id()
        {
            Assert.That(Reg9ToBeReviewed.FirstOrDefault().BranchId, Is.EqualTo(111));
        }

        [Test]
        public void then_the_added_Reg9_should_have_this_frequency_id()
        {
            Assert.That(Reg9ToBeReviewed.FirstOrDefault().Reg9FrequencyTypeId, Is.EqualTo(1));
        }

        [Test]
        public void then_the_added_Reg9_should_have_this_review_due_date()
        {
            Assert.That(Reg9ToBeReviewed.FirstOrDefault().DueDate.ToString("MM/dd/yyyy"), Is.EqualTo("01/01/2014"));
        }

        [Test]
        public void then_the_added_Reg9_should_have_this_branch_template_id()
        {
            Assert.That(Reg9ToBeReviewed.FirstOrDefault().BranchReg9Id, Is.EqualTo(1));
        }

        [Test]
        public void then_the_added_Reg9_should_have_this_approver()
        {
            Assert.That(Reg9ToBeReviewed.FirstOrDefault().Approver, Is.EqualTo("XXX"));
        }

        [Test]
        public void then_the_added_Reg9_should_have_this_reviewer()
        {
            Assert.That(Reg9ToBeReviewed.FirstOrDefault().Reviewer, Is.EqualTo("XZY"));
        }

        [Test]
        public void then_the_added_Reg9_should_have_this_notes()
        {
            Assert.That(Reg9ToBeReviewed.FirstOrDefault().Notes, Is.EqualTo("Some test notes"));
        }

    }

    public abstract class SchedulingReg9FailureHandlerContext : TestBase
    {
        protected SchedulingReg9FailureHandlerContext()
        {
            Describes("Scheduling a Reg 9 for review should throw validation exceptions");
        }

        protected Reg9CompositesToBeScheduled Reg9CompositesToBeScheduled { get; set; }
        protected DateTime Now { get; set; }
        protected IAllocationContextDb AllocationContextDb { get; set; }
        protected IUserContext UserContext { get; set; }
        protected ILocalTimeService TimeService { get; set; }
        protected IBranchService BranchService { get; set; }
        protected ICommandHandler<SaveReg9ScheduleComposites, CommandHandlerResult> Subject { get; set; }
        protected CommandHandlerValidation ExpectedException { get; set; }
        protected Exception Exception { get; set; }

        protected void EstablishContext()
        {
            Now = DateTime.Now;
            AllocationContextDb = Substitute.For<IAllocationContextDb>();
            UserContext = Substitute.For<IUserContext>();
            TimeService = Substitute.For<ILocalTimeService>();
            BranchService = Substitute.For<IBranchService>();
            BranchService.GetPortfolioBranchId(12345).Returns(111);
            Subject = new SaveReg9ScheduleCompositesHandler(AllocationContextDb, UserContext, TimeService, BranchService);
        }

        protected void given_the_reg9_to_be_scheduled_exists()
        {
            Reg9CompositesToBeScheduled = new Reg9CompositesToBeScheduled
            {
                Reg9CompositesToBeScheduledId = 11,
                BranchId = 337,
                CompositeId = 12345,
                Reg9FrequencyTypeId = 1,
                BranchReg9Id = 1,
                DueDate = Convert.ToDateTime("01/01/2014"),
                Reviewer = "ABC",
                Approver = "XYZ",
                Notes = "Test notes"
            };
            Func<object[], IQueryable<Reg9CompositesToBeScheduled>, Reg9CompositesToBeScheduled> finder = (keys, query) =>
            {
                var id = (int)keys[0];
                return query.First(x => x.Reg9CompositesToBeScheduledId == id);
            };
            var dbSet = new FakeDbSet<Reg9CompositesToBeScheduled>(finder) { Reg9CompositesToBeScheduled };
            AllocationContextDb.Reg9CompositesToBeScheduled.Returns(dbSet);
        }

        //protected void given_the_reg9_scheduler_throws_exception()
        //{
        //    ExpectedException = new CommandHandlerException
        //    {
        //        ExMessage = "A Reg 9 for this frequency and Due Date has already been scheduled."
        //    };
        //    try
        //    {
        //        Subject.Process(new SaveReg9ScheduleComposites
        //            {
        //                Reg9ScheduleCompositesId = -1,
        //                BranchId = 337,
        //                CompositeId = 12345,
        //                ReviewFrequencyId = 1,
        //                BranchReg9Id = 1,
        //                ReviewDueDate = "01/01/2014",
        //                Reviewer = "XZY",
        //                Approver = "XXX",
        //                Notes = "Some test notes"
        //            }).Returns(x => { throw ExpectedException; });
        //    }
        //    catch (Exception e)
        //    {
        //        Exception = e;
        //    }
        //}
    }

    public abstract class SchedulingReg9SuccessHandlerContext : TestBase
    {
        protected SchedulingReg9SuccessHandlerContext()
        {
            Describes("Successfully scheduling a Reg 9 review");
        }

        protected void EstablishContext()
        {
            Now = DateTime.Now;
            AllocationContextDb = Substitute.For<IAllocationContextDb>();
            UserContext = Substitute.For<IUserContext>();
            TimeService = Substitute.For<ILocalTimeService>();
            BranchService = Substitute.For<IBranchService>();
            BranchService.GetPortfolioBranchId(12345).Returns(111);
            Reg9ToBeReviewed = new FakeDbSet<Reg9CompositesToBeScheduled>();
            AllocationContextDb.Reg9CompositesToBeScheduled.Returns(Reg9ToBeReviewed);

            Subject = new SaveReg9ScheduleCompositesHandler(AllocationContextDb, UserContext, TimeService, BranchService);
        }


        protected Reg9CompositesToBeScheduled Reg9CompositesToBeScheduled { get; set; }
        protected DateTime Now { get; set; }
        protected IAllocationContextDb AllocationContextDb { get; set; }
        protected IUserContext UserContext { get; set; }
        protected ILocalTimeService TimeService { get; set; }
        protected IBranchService BranchService { get; set; }
        protected ICommandHandler<SaveReg9ScheduleComposites, CommandHandlerResult> Subject { get; set; }
        protected IDbSet<Reg9CompositesToBeScheduled> Reg9ToBeReviewed { get; set; }

        protected void when_the_reg9_to_be_scheduled_is_saved()
        {
            Subject.Process(new SaveReg9ScheduleComposites
            {
                Reg9ScheduleCompositesId = -1,
                BranchId = 337,
                CompositeId = 12345,
                ReviewFrequencyId = 1,
                BranchReg9Id = 1,
                ReviewDueDate = "01/01/2014",
                Reviewer = "XZY",
                Approver = "XXX",
                Notes = "Some test notes"
            });

            Reg9ToBeReviewed.Add(AllocationContextDb.Reg9CompositesToBeScheduled.FirstOrDefault());
        }
    } 
}
