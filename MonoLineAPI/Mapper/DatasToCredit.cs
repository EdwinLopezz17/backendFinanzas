using AutoMapper;
using MonoLineAPI.Request;
using MonolineInfraestructure.models;

namespace MonoLineAPI.Mapper;

public class DatasToCredit: Profile
{
    public DatasToCredit()
    {
        CreateMap<Datas, Credit>();
    }
}