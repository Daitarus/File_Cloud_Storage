namespace RepositoryDB
{
    public class RepositoryClientFile : Repository<ClientFile>, IRepositoryClientFile
    {

        public List<int>? IdFileForClient(int idClient)
        {
            IQueryable<ClientFile> clientFileCollection = db.FilesAttach.Where(clientFile => clientFile.Id_Client.Equals(idClient));
            if(clientFileCollection.Count() > 0)
            {
                List<int> allIdFile = new List<int>();
                foreach (var clientFile in clientFileCollection)
                {
                    allIdFile.Add(clientFile.Id_File);
                }
                return allIdFile;
            }
            else
            {
                return null;
            }
        }
    }
}
