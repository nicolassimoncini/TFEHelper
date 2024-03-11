namespace TFEHelper.Backend.Core.API.SpringerLink.Classes
{
    public static class SpringerLinkAPIWrapperExtensions
    {
        public static SpringerLinkAPIWrapper WithDateRange(this SpringerLinkAPIWrapper client, DateTime dateFrom, DateTime dateTo)
        {
            return client;
        }

        public static SpringerLinkAPIWrapper WithQuery(this SpringerLinkAPIWrapper client, string query)
        {
            return client;
        }

        public static SpringerLinkAPIWrapper WithSubject(this SpringerLinkAPIWrapper client, string subject)
        {
            return client;
        }
    }
}
