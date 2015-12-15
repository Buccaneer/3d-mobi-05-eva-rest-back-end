using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Dependencies;

namespace EVARest.App_Start {
    public class NinjectDependencyResolver : NinjectDependencyScope, IDependencyResolver {
        private readonly IKernel _kernel;

        public NinjectDependencyResolver(IKernel kernel)
            : base(kernel) {
            _kernel = kernel;
        }

        public IDependencyScope BeginScope() {
            return new NinjectDependencyScope(_kernel.BeginBlock());
        }
    }
}
