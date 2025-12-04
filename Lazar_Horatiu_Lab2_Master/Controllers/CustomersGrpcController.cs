using Microsoft.AspNetCore.Mvc;
using Grpc.Net.Client;
using GrpcCustomersService;
using Lazar_Horatiu_Lab2_Master.Models;
using Customer = GrpcCustomersService.Customer;

namespace Lazar_Horatiu_Lab2_Master.Controllers
{
    public class CustomersGrpcController : Controller
    {
        private readonly GrpcChannel channel;
        public CustomersGrpcController()
        {
            //a se modifica portul corespunzator (vezi in proiectul GrpcCustomerService-> Properties->launchSettings.json)
            channel = GrpcChannel.ForAddress("https://localhost:7189");
        }
        [HttpGet]
        public IActionResult Index()
        {
            var client = new CustomerService.CustomerServiceClient(channel);
            CustomerList cust = client.GetAll(new Empty());
            return View(cust);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                var client = new
                CustomerService.CustomerServiceClient(channel);
                var createdCustomer = client.Insert(customer);
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var client = new CustomerService.CustomerServiceClient(channel);
            Customer customer = client.Get(new CustomerId() { Id = (int)id });
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var client = new CustomerService.CustomerServiceClient(channel);
            Empty response = client.Delete(new CustomerId()
            {
                Id = id
            });
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var client = new CustomerService.CustomerServiceClient(channel);
            Customer grpcCustomer = client.Get(new CustomerId() { Id = (int)id });
            if (grpcCustomer == null)
            {
                return NotFound();
            }

            var modelCustomer = new Lazar_Horatiu_Lab2_Master.Models.Customer
            {
                CustomerID = grpcCustomer.CustomerId,
                Name = grpcCustomer.Name,
                Adress = grpcCustomer.Adress,
                BirthDate = DateTime.Parse(grpcCustomer.Birthdate)
            };

            return View(modelCustomer);
        }

        [HttpPost]
        public IActionResult Update(Lazar_Horatiu_Lab2_Master.Models.Customer customer)
        {
            if (ModelState.IsValid)
            {
                var client = new CustomerService.CustomerServiceClient(channel);

                var grpcCustomer = new Customer
                {
                    CustomerId = customer.CustomerID,
                    Name = customer.Name,
                    Adress = customer.Adress,
                    Birthdate = customer.BirthDate.ToString("yyyy-MM-dd")
                };

                client.Update(grpcCustomer);
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }
    }
}