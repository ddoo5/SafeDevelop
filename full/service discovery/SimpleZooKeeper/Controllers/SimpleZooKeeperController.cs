using Microsoft.AspNetCore.Mvc;

namespace SimpleZooKeeper.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SimpleZooKeeperController : ControllerBase
    {
        private readonly ILogger<SimpleZooKeeperController> _logger;
        private readonly ServiceRepository serviceRepository;

        public SimpleZooKeeperController(ILogger<SimpleZooKeeperController> logger, ServiceRepository serviceRepository)
        {
            _logger = logger;
            this.serviceRepository = serviceRepository;
        }

        [HttpGet]
        public IEnumerable<SelfRegistrationData> Get()
        {
            return serviceRepository.GetAllServices();
        }

        [HttpPost]
        public SelfRegistrationData RegistryService([FromBody] SelfRegistrationData selfRegistrationData)
        {
            return serviceRepository.AddService(selfRegistrationData);
            return new SelfRegistrationData();
        }
    }
}