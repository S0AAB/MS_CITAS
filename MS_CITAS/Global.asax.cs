using MS_CITAS.App_Start;
using System.Web.Http;


namespace MS_CITAS
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
        
            GlobalConfiguration.Configure(WebApiConfig.Register);
            AutofacConfig.Register();
        
          
        }
    }
}
