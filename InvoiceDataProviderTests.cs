using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fortigent.Bruce.Logic.Firm.Providers.Billing;
using Fortigent.Bruce.Logic.Firm.ViewModels.Billing;
using Fortigent.Domains.Objects.Branches;
using Fortigent.Domains.Objects.ClientBilling;
using Fortigent.Domains.Objects.Clients;
using Fortigent.Domains.Repository;
using Fortigent.TDDUtilities;
using NUnit.Framework;
using NSubstitute;

namespace Fortigent.Bruce.Tests.ClientBilling.Providers
{
	[TestFixture]
	[Category("Db_Free")]
    public class InvoiceDataProviderTests
    {
        private IAllocationContextDb _db;
        private IInvoiceDataProvider _subject;
       

        [TestFixtureSetUp]
        public void Setup()
        {
            _db = Substitute.For<IAllocationContextDb>();
            _subject = new InvoiceDataProvider(_db);
            IDbSet<Branch> branches = new FakeDbSet<Branch>();
            branches.Add(new Branch()
            {
                BranchID = 7000,
                BranchName = "A Branch"
            });
            branches.Add(new Branch()
            {
                BranchID = 7001,
                BranchName = "A Child Branch",
                ParentBranchID = 7000
            });
            branches.Add(new Branch()
            {
                BranchID = 8000,
                BranchName = "Another branch"

            });
            _db.Branches.ReturnsForAnyArgs(branches);
            IDbSet<ClientGroup> clientGroups = new FakeDbSet<ClientGroup>();
            clientGroups.Add(new ClientGroup()
            {
                Branch = branches.FirstOrDefault(b => b.BranchID== 7000),
                ClientGroupId = 5600,
                IsProspect = false,
                ClientGroupName = "Fredo Corleone"
            });
            clientGroups.Add(new ClientGroup()
            {
                Branch = branches.FirstOrDefault(b => b.BranchID == 7001),
                ClientGroupId = 5700,
                IsProspect = false,
                ClientGroupName = "Bunny Ziffer"
            });

            clientGroups.Add(new ClientGroup()
            {
                Branch = branches.FirstOrDefault(b => b.BranchID == 8000),
                ClientGroupId = 5800,
                IsProspect = false,
                ClientGroupName = "Rollo Tomassi"
            });

            _db.ClientGroups.ReturnsForAnyArgs(clientGroups);

            var query = (clientGroups.Select(x => x));
            _db.ApplyDataSetPermissions(Arg.Any<Func<IAllocationContextDb, IQueryable<ClientGroup>>>())
                 .Returns(query);

            IDbSet<Setting> settings = new FakeDbSet<Setting>();
            settings.Add(new Setting()
            {
                SettingId = 560099,
                SettingName = "A test Setting",
                ClientGroup = clientGroups.FirstOrDefault(cg => cg.ClientGroupId == 5600)
            });
            settings.Add(new Setting()
            {
                SettingId = 560098,
                SettingName = "A Deleted test Setting",
                ClientGroup = clientGroups.FirstOrDefault(cg => cg.ClientGroupId == 5600),
                DeletedByUserId = -9999,
                DeletedDate = DateTime.Now
            });

            settings.Add(new Setting()
            {
                SettingId = 570099,
                SettingName = "Another test Setting",
                ClientGroup = clientGroups.FirstOrDefault(cg => cg.ClientGroupId == 5700)
            });

            settings.Add(new Setting()
            {
                SettingId = 580099,
                SettingName = "Usual Setting",
                ClientGroup = clientGroups.FirstOrDefault(cg => cg.ClientGroupId == 5800)
            });

            _db.Settings.ReturnsForAnyArgs(settings);

            IDbSet<Invoice> invoices = new FakeDbSet<Invoice>();
            invoices.Add(new Invoice()
            {
                InvoiceId = 1,
                Amount = 10000,
                InvoiceFileUid = Guid.NewGuid(),
                InvoiceDate = new DateTime(2013,12,31),
                Setting = settings.FirstOrDefault(s => s.SettingId == 560099)
            });
            invoices.Add(new Invoice()
            {
                InvoiceId = 2,
                Amount = 20000,
                InvoiceFileUid = Guid.NewGuid(),
                InvoiceDate = new DateTime(2013, 12, 31),
                Setting = settings.FirstOrDefault(s => s.SettingId == 570099)
            });

            invoices.Add(new Invoice()
            {
                InvoiceId = 3,
                Amount = 30000,
                InvoiceFileUid = Guid.NewGuid(),
                InvoiceDate = new DateTime(2013, 12, 31),
                Setting = settings.FirstOrDefault(s => s.SettingId == 580099)
            });

            invoices.Add(new Invoice()
            {
                InvoiceId = 4,
                Amount = 30000,
                InvoiceFileUid = Guid.NewGuid(),
                InvoiceDate = new DateTime(2013, 12, 31),
                Setting = settings.FirstOrDefault(s => s.SettingId == 560098)
            });
            invoices.Add(new Invoice()
            {
                InvoiceId = 11,
                Amount = 12000,
                InvoiceFileUid = Guid.NewGuid(),
                InvoiceDate = new DateTime(2014, 3, 31),
                Setting = settings.FirstOrDefault(s => s.SettingId == 560099),
                ModifiedAmount = 56,
                ModifiedInvoiceFileUid = Guid.NewGuid()
            });

            _db.Invoices.ReturnsForAnyArgs(invoices);

             
        }

        [Test]
        public void ShouldReturnInvoiceDataForSuppliedInvoiceList()
        {

          

            List<int> invoiceIds = new List<int>();
            invoiceIds.Add(1);
            DirectBillsQuery query = new DirectBillsQuery()
            {
                InvoiceIds = invoiceIds
            };

            DirectBillsViewModel vm = new DirectBillsViewModel()
            {
                BranchId = 7000
            };

            IList<InvoiceData> result = _subject.Get(vm, query);
            Assert.AreEqual(invoiceIds.Count, result.Count);
        }

        [Test]
        public void ShouldNotReturnInvoiceDataIfInvoiceIDNotSupplied()
        {



            List<int> invoiceIds = new List<int>();
            invoiceIds.Add(-1);
            DirectBillsQuery query = new DirectBillsQuery()
            {
                InvoiceIds = invoiceIds
            };

            DirectBillsViewModel vm = new DirectBillsViewModel()
            {
                BranchId = 7000
            };

            IList<InvoiceData> result = _subject.Get(vm, query);
            Assert.AreEqual(0, result.Count);
        }


        [Test]
        public void ShouldReturnInvoiceDataForSuppliedInvoiceListIncludingChildBranches()
        {

            List<int> invoiceIds = new List<int>();
            invoiceIds.Add(1);
            invoiceIds.Add(2);
            DirectBillsQuery query = new DirectBillsQuery()
            {
                InvoiceIds = invoiceIds
            };

            DirectBillsViewModel vm = new DirectBillsViewModel()
            {
                BranchId = 7000
            };

            IList<InvoiceData> result = _subject.Get(vm, query);
            Assert.AreEqual(invoiceIds.Count, result.Count);
        }

        [Test]
        public void ShouldNotReturnInvoiceDataForNonRelatedBranches()
        {
            List<int> invoiceIds = new List<int>();
            invoiceIds.Add(1);
            invoiceIds.Add(2);
            invoiceIds.Add(3);//from other branch
            DirectBillsQuery query = new DirectBillsQuery()
            {
                InvoiceIds = invoiceIds
            };

            DirectBillsViewModel vm = new DirectBillsViewModel()
            {
                BranchId = 7000
            };

            IList<InvoiceData> result = _subject.Get(vm, query);
            Assert.AreEqual(2, result.Count);
        }

        [Test]
        public void ShouldNotIncludeInvoicesTiedToDeletedSettings()
        {
            List<int> invoiceIds = new List<int>();
            invoiceIds.Add(1);
            invoiceIds.Add(4);
            DirectBillsQuery query = new DirectBillsQuery()
            {
                InvoiceIds = invoiceIds
            };

            DirectBillsViewModel vm = new DirectBillsViewModel()
            {
                BranchId = 7000
            };

            IList<InvoiceData> result = _subject.Get(vm, query);
            Assert.AreEqual(1, result.Count);
        }

        [Test]
        public void ShouldUseModifiedInvoiceFileIfAvailable()
        {
            List<int> invoiceIds = new List<int>();
            invoiceIds.Add(11);
            DirectBillsQuery query = new DirectBillsQuery()
            {
                InvoiceIds = invoiceIds
            };

            DirectBillsViewModel vm = new DirectBillsViewModel()
            {
                BranchId = 7000
            };

            IList<InvoiceData> result = _subject.Get(vm, query);
            Guid modifiedGuid = _db.Invoices.FirstOrDefault(i => i.InvoiceId == 11).ModifiedInvoiceFileUid.Value;
            Assert.AreEqual(modifiedGuid, result[0].InvoiceFileUid);
            Guid originalGuid = _db.Invoices.FirstOrDefault(i => i.InvoiceId == 11).InvoiceFileUid.Value;
            Assert.AreNotEqual(originalGuid, result[0].InvoiceFileUid);
        }

        [Test]
        public void ShouldUseOriginalInvoiceFileIfModifedFileNotAvailable()
        {
            List<int> invoiceIds = new List<int>();
            invoiceIds.Add(1);
            DirectBillsQuery query = new DirectBillsQuery()
            {
                InvoiceIds = invoiceIds
            };

            DirectBillsViewModel vm = new DirectBillsViewModel()
            {
                BranchId = 7000
            };

            IList<InvoiceData> result = _subject.Get(vm, query);
            Guid originalGuid = _db.Invoices.FirstOrDefault(i => i.InvoiceId == 1).InvoiceFileUid.Value;
            Assert.AreEqual(originalGuid, result[0].InvoiceFileUid);
        }

        [Test]
        public void ShouldUseModifiedInvoiceAmountIfAvailable()
        {
            List<int> invoiceIds = new List<int>();
            invoiceIds.Add(11);
            DirectBillsQuery query = new DirectBillsQuery()
            {
                InvoiceIds = invoiceIds
            };

            DirectBillsViewModel vm = new DirectBillsViewModel()
            {
                BranchId = 7000
            };

            IList<InvoiceData> result = _subject.Get(vm, query);
            decimal modifiedAmount = _db.Invoices.FirstOrDefault(i => i.InvoiceId == 11).ModifiedAmount.Value;
            Assert.AreEqual(modifiedAmount, result[0].InvoiceTotal);
            decimal originalAmount = _db.Invoices.FirstOrDefault(i => i.InvoiceId == 11).Amount;
            Assert.AreNotEqual(originalAmount, result[0].InvoiceTotal);
        }

        [Test]
        public void ShouldUseOriginalInvoiceAmountIfNoModifiedAmountAvailable()
        {
            List<int> invoiceIds = new List<int>();
            invoiceIds.Add(1);
            DirectBillsQuery query = new DirectBillsQuery()
            {
                InvoiceIds = invoiceIds
            };

            DirectBillsViewModel vm = new DirectBillsViewModel()
            {
                BranchId = 7000
            };

            IList<InvoiceData> result = _subject.Get(vm, query);
            decimal originalAmount = _db.Invoices.FirstOrDefault(i => i.InvoiceId == 1).Amount;
            Assert.AreEqual(originalAmount, result[0].InvoiceTotal);
        }
    }
}
