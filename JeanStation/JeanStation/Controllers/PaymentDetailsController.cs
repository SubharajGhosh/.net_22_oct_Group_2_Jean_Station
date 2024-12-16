using JeanStation.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace JeanStation.Controllers
{
    [RoutePrefix("api/PaymentDetails")]
    public class PaymentDetailsController : ApiController
    {
        private IPaymentDetailsRepository _paymentDetailsRepository;

        public PaymentDetailsController()
        {
            _paymentDetailsRepository = new PaymentDetailsRepository();
        }

        // Process a payment
        [HttpPost]
        [Route("ProcessPayment")]
        public IHttpActionResult ProcessPayment(string orderId, string paymentMode)
        {
            try
            {
                var paymentDetails = _paymentDetailsRepository.ProcessPayment(orderId, paymentMode);
                return Ok(paymentDetails);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // Get payment details by Order ID
        [HttpGet]
        [Route("GetPaymentByOrderId/{orderId}")]
        public IHttpActionResult GetPaymentByOrderId(string orderId)
        {
            try
            {
                var paymentDetails = _paymentDetailsRepository.GetPaymentByOrderId(orderId);
                if (!paymentDetails.Any())
                {
                    return NotFound();
                }
                return Ok(paymentDetails);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        
        
    }
}

