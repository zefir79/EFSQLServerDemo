using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFSQLServerDemo.Domain.Object;

namespace EFSQLServerDemo.Business.Test.DataBuilder
{
    public class OrderPaymentBuilder
    {
        private int _orderPaymentId = -1;
        private int _orderId = -1;
        private int _paymentModeId = -1;
        private int _cardNumber = -1;
        private string _cardName = null;
        private string _ccv = null;
        private string _expirationDate;
        private decimal _paymentAmount = Convert.ToDecimal("0.00");
        private DateTime? _processingDate = null;

        public OrderPayment Build()
        {
            return new OrderPayment
            {
                OrderPaymentId = -1,
                OrderId = _orderId,
                PaymentModeId = _paymentModeId,
                CardNumber = _cardNumber,
                CardName = _cardName,
                CCV = _ccv,
                ExpirationDate = _expirationDate,
                PaymentAmount = _paymentAmount,
                ProcessingDate = _processingDate
            };
        }

        public OrderPaymentBuilder WithOrderPaymentId(int newOrderPaymentId)
        {
            _orderPaymentId = newOrderPaymentId;
            return this;
        }

        public OrderPaymentBuilder WithOrderId(int newOrderId)
        {
            _orderId = newOrderId;
            return this;
        }

        public OrderPaymentBuilder WithPaymentModeId(int newPaymentModeId)
        {
            _paymentModeId = newPaymentModeId;
            return this;
        }

        public OrderPaymentBuilder WithCardNumber(int newCardNumber)
        {
            _cardNumber = newCardNumber;
            return this;
        }


        public OrderPaymentBuilder WithCardName(string newCardName)
        {
            _cardName = newCardName;
            return this;
        }

        public OrderPaymentBuilder WithCCV(string newCcv)
        {
            _ccv = newCcv;
            return this;
        }

        public OrderPaymentBuilder WithExpirationDate(string newExpirationDate)
        {
            _expirationDate = newExpirationDate;
            return this;
        }

        public OrderPaymentBuilder WithPaymentAmount(decimal newPaymentAmount)
        {
            _paymentAmount = newPaymentAmount;
            return this;
        }

        public OrderPaymentBuilder WithProcessingDate(DateTime? newProcessingDate)
        {
            _processingDate = newProcessingDate;
            return this;
        }
    }
}
