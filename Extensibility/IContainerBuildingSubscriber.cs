namespace Glitonea.Extensibility;

using Autofac;

public interface IContainerBuildingSubscriber
{
    void OnBuildingIoC(ContainerBuilder containerBuilder);
}