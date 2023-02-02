using Autofac;
using AutoMapper;
using HR_Plus.Domain.Repositories;
using HR_PLUS.Application.AutoMapper;
using HR_PLUS.Application.Services.AdvancePaymentService;
using HR_PLUS.Application.Services.AppUserService;
using HR_PLUS.Application.Services.CompanyService;
using HR_PLUS.Application.Services.DashboardService;
using HR_PLUS.Application.Services.EmployeeService;
using HR_PLUS.Application.Services.ExpenseService;
using HR_PLUS.Application.Services.ManagerService;
using HR_PLUS.Application.Services.PermissionService;
using HR_PLUS.Application.Services.PermissionTypeService;
using HR_PLUS.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_PLUS.Application.IoC
{
    public class DependencyResolver:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
           
            builder.RegisterType<CompanyRepository>().As<ICompanyRepository>().InstancePerLifetimeScope();
            builder.RegisterType<PermissionRepository>().As<IPermissionRepository>().InstancePerLifetimeScope();
            builder.RegisterType<PermissionTypeRepository>().As<IPermissionTypeRepository>().InstancePerLifetimeScope();
            builder.RegisterType<AdvancePaymentRepository>().As<IAdvancePaymentRepository>().InstancePerLifetimeScope();
            builder.RegisterType<AppUserRepository>().As<IAppUserRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ExpenseRepository>().As<IExpenseRepository>().InstancePerLifetimeScope();

            builder.RegisterType<Mapper>().As<IMapper>().InstancePerLifetimeScope();
                     
            builder.RegisterType<PermissionService>().As<IPermissionService>().InstancePerLifetimeScope();
            builder.RegisterType<PermissionTypeService>().As<IPermissionTypeService>().InstancePerLifetimeScope();
            builder.RegisterType<AdvancePaymentService>().As<IAdvancePaymentService>().InstancePerLifetimeScope();
            builder.RegisterType<DashboardService>().As<IDashboardService>().InstancePerLifetimeScope();
            builder.RegisterType<AppUserService>().As<IAppUserService>().InstancePerLifetimeScope();
            builder.RegisterType<ExpenseService>().As<IExpenseService>().InstancePerLifetimeScope();
            builder.RegisterType<EmployeeService>().As<IEmployeeService>().InstancePerLifetimeScope();
            builder.RegisterType<CompanyService>().As<ICompanyService>().InstancePerLifetimeScope();
            builder.RegisterType<ManagerService>().As<IManagerService>().InstancePerLifetimeScope();

            #region AutoMapper
            builder.Register(context => new MapperConfiguration(cfg =>
            {
                //Register Mapper Profile
                cfg.AddProfile<Mapping>(); //Automapper klosorunun altına  ekledııgmız zaman mapping classını baglıyoruz.
            }
           )).AsSelf().SingleInstance();

            builder.Register(c =>
            {
                //This resolves a new context that can be used later.
                var context = c.Resolve<IComponentContext>();
                var config = context.Resolve<MapperConfiguration>();
                return config.CreateMapper(context.Resolve);
            })
            .As<IMapper>()
            .InstancePerLifetimeScope();
            #endregion

            base.Load(builder);
        }
    }
}
