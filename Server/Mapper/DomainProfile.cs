using AutoMapper;
using TodoApi.Server.Models;
using TodoApi.Shared.Models;

public class DomainProfile : Profile
{
	public DomainProfile()
	{
		CreateMap<TodoItem, TodoViewModel>();
		CreateMap<TodoViewModel, TodoItem>();
		CreateMap<WebApiUser, RegisterViewModel>();
		CreateMap<RegisterViewModel, WebApiUser>();
		CreateMap<WebApiUser, DisplayViewModel>();
		CreateMap<DisplayViewModel, WebApiUser>();
	}
}