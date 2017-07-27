using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Ninject;
using System.Web.Mvc;
using EachVoice.Models.CustomU;
using EachVoice.Abstract;
using Moq;
using Ninject.Web.Common;
using EachVoice.ConcreteRepo;
using EachVoice.Models;

namespace EachVoice.Infrastructure
{
    //IdependencyResolver straight from mvc
    public class NinjectResolver : IDependencyResolver
    {
        private IKernel kernel;

        //init kernel
        public NinjectResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            kernel.Bind<UCContext>().ToSelf().InRequestScope(); ;
            kernel.Bind<ApplicationDbContext>().ToSelf().InRequestScope();


            kernel.Bind<IUCCRepository>().To<UCCRepository>();
            kernel.Bind<IAdminRepo>().To<AdminRepo>();
        }
    }
}