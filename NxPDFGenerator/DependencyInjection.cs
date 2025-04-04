using System.Reflection;

namespace NxPDFGenerator
{
    public static class DependencyInjection
    {
        public static IApplicationBuilder RegisterEndpoints(this IApplicationBuilder app)
        {
            var mapEndpointMethods = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => t.Namespace == "NxPDFGenerator.Endpoints" && t.GetMethod("MapEndpoints") != null)
                .Select(x => x.GetMethod("MapEndpoints"));

            foreach (var m in mapEndpointMethods)
            {
                if (m != null)
                    m.Invoke(null, new object[] { app });
            }

            return app;
        }
    }
}
