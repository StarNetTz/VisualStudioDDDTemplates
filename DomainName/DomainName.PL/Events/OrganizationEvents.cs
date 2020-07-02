namespace $safeprojectname$
{
    public class OrganizationRegistered : Event
    {
        public string Name { get; set; }
        public Address Address { get; set; }
    }

    public class OrganizationNameCorrected : Event
    {
        public string Name { get; set; }
    }
}