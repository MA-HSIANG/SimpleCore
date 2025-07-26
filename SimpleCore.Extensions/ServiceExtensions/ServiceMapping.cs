
using AutoMapper;
using SimpleCore.Model;
using SimpleCore.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCore.Extensions.ServiceExtensions
{
    public class ServiceMapping : Profile
    {
        public ServiceMapping()
        {
            CreateMap<UserIninfoModel, UserIninfoViewModel>();
            CreateMap<UserIninfoViewModel, UserIninfoModel>();

 
        }
    }
}
