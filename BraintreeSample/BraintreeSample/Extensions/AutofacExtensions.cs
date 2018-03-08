﻿using Autofac;
using BraintreeSample.Services.RequestProvider;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Xamarin.Forms;

namespace BraintreeSample.Extensions
{
  public static class AutofacExtensions
  {

    public static ContainerBuilder RegisterViewModels(this ContainerBuilder builder, params Assembly[] assemblies)
    {
      builder
          .RegisterAssemblyTypes(assemblies)
          .Where(x =>
              x.GetTypeInfo().IsClass &&
             !x.GetTypeInfo().IsAbstract &&
              x.Name.EndsWith("ViewModel"))
          .InstancePerDependency();

      return builder;
    }


    public static ContainerBuilder RegisterViews(this ContainerBuilder builder, params Assembly[] assemblies)
    {
      builder
          .RegisterAssemblyTypes(assemblies)
          .Where(x =>
              x.GetTypeInfo().IsClass &&
              !x.GetTypeInfo().IsAbstract &&
              x.GetTypeInfo().IsSubclassOf(typeof(Page))
          )
          .InstancePerDependency();

      return builder;
    }


    public static ContainerBuilder RegisterServices(this ContainerBuilder builder, params Assembly[] assemblies)
    {
      builder.RegisterAssemblyTypes(assemblies)
          .Where(t => t.Name.EndsWith("Service"))
          .WithParameter("requestProvider", new RequestProvider())
          .AsImplementedInterfaces();

      return builder;
    }
  }
}
