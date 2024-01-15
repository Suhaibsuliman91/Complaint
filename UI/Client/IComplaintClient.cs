namespace UI.Client
{
    public interface IComplaintClient:IClient<DTO.Complaint>
    {
        public Task<DTO.FileInfo> DownloadFile(string FileName);
    }
}
