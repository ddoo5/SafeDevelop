namespace SimpleZooKeeper
{
    public class ServiceRepository
    {
        private List<SelfRegistrationData> selfRegistrationDatasRepository;
        public ServiceRepository()
        {
            selfRegistrationDatasRepository = new List<SelfRegistrationData>();
        }

        public SelfRegistrationData AddService(SelfRegistrationData selfRegistrationData)
        {
            selfRegistrationData.Created=DateTime.Now;

            selfRegistrationDatasRepository.Add(selfRegistrationData);

            return selfRegistrationData;
        }

        public void RemoveService(SelfRegistrationData selfRegistrationData)
        {
            selfRegistrationDatasRepository.RemoveAll(x => x.Name == selfRegistrationData.Name);
        }

        public IEnumerable<SelfRegistrationData> GetAllServices() => selfRegistrationDatasRepository;
        public SelfRegistrationData? GetOne() => selfRegistrationDatasRepository.FirstOrDefault(x => x.Created == selfRegistrationDatasRepository.Max(x => x.Created));
    }
}
