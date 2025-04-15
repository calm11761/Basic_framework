using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basis__Common
{
        [ApiController]
        public class BaseController<T> : ControllerBase where T : class
        {
            protected readonly T Service;

            public BaseController(T _service)
            {
                Service = _service;
            }
        }
}
