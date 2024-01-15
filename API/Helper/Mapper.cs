using AutoMapper;

namespace API.Helper
{
    public class Mapper:Profile
    {
        public Mapper()
        {
            CreateMap<Data.User, DTO.User>();
            CreateMap<DTO.User, Data.User>()
                .AfterMap((Source, Destination) =>
                {
                    if (Destination.ID == 0)
                    {
                        Destination.IsDeleted = false;
                    }
                });

            CreateMap<Data.UserType, DTO.UserType>();
            CreateMap<DTO.UserType, Data.UserType>()
                .AfterMap((Source, Destination) =>
                {
                    if (Destination.ID == 0)
                    {
                        Destination.IsDeleted = false;
                    }
                });

            CreateMap<Data.Complaint, DTO.Complaint>();
            CreateMap<DTO.Complaint, Data.Complaint>()
                .AfterMap((Source, Destination) =>
                {
                    if (Destination.ID == 0)
                    {
                        Destination.IsDeleted = false;
                    }
                });

            CreateMap<Data.Demand, DTO.Demand>();
            CreateMap<DTO.Demand, Data.Demand>()
                .AfterMap((Source, Destination) =>
                {
                    if (Destination.ID == 0)
                    {
                        Destination.IsDeleted = false;
                    }
                });

            CreateMap<Data.Status, DTO.Status>();
            CreateMap<DTO.Status, Data.Status>()
                .AfterMap((Source, Destination) =>
                {
                    if (Destination.ID == 0)
                    {
                        Destination.IsDeleted = false;
                    }
                });
        }
    }
}
