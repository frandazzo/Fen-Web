using System;
using System.Collections.Generic;
using System.Text;
using WIN.TECHNICAL.SECURITY_NEW.RoleManagement;
using System.Reflection;
using System.IO;
using WIN.FENGEST_NAZIONALE.DOMAIN.Serializzation;

namespace WIN.FENGEST_NAZIONALE.DOMAIN.Security
{
    public class RoleProvider : IRoleProvider
    {
        private string _fileName = "Ruoli.xml";
        private IList<Role> result = null;


        #region IRoleProvider Membri di

        public IList<Role> GetRoles()
        {

            if (result != null)
                return result;


            //Recupero il path del file xml dei ruoli
            string file = Assembly.GetExecutingAssembly().CodeBase.Replace("file:///", "");
            FileInfo f = new FileInfo(file);
            string path = Path.Combine (f.DirectoryName ,_fileName );

            if (!File.Exists(path))
                throw new FileNotFoundException("File dei ruoli non trovato");

            RoleDescriptor r = ObjectXMLSerializer<RoleDescriptor>.Load(path);

            if (r == null)
                return new List<Role>();

            result = new List<Role>();

            foreach (Role item in r.Roles)
            {
                result.Add(item);
            }

            return result;
        }

        #endregion


        public IList<Profile> GetProfiles()
        {
            List<Profile> result = new List<Profile>();

            IList<Role> roles = this.GetRoles();

            foreach (Role item in roles)
            {
                TryAddProfile(result, item.Profiles);
            }


            result.Sort(Profile.CompareResourcesByName);

            return result;

        }

        private void TryAddProfile(IList<Profile> result, Profile[] profiles)
        {
            foreach (Profile item in profiles)
            {
                if (!ContainsProfile(result, item))
                    result.Add(item);
            }
        }

        private bool ContainsProfile(IList<Profile> result, Profile profile)
        {
            foreach (Profile item in result)
            {
                if (item.Name.ToLower() == profile.Name.ToLower())
                    return true;
            }

            return false;
        }

        public IList<string> GetProfileNameList()
        {
            IList<string> profiles = new List<string>();

            foreach (Profile item in GetProfiles())
            {
                profiles.Add(item.Name);
            }

            return profiles;

        }
    }
}
