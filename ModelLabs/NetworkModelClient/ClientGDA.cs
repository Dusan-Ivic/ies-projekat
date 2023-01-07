using FTN.Common;
using FTN.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkModelClient
{
    public class ClientGDA : IDisposable
    {
        private ModelResourcesDesc modelResourcesDesc = new ModelResourcesDesc();
        private NetworkModelGDAProxy gdaQueryProxy = null;

        public ClientGDA()
        {

        }

        private NetworkModelGDAProxy GdaQueryProxy
        {
            get
            {
                if (gdaQueryProxy != null)
                {
                    gdaQueryProxy.Abort();
                    gdaQueryProxy = null;
                }

                gdaQueryProxy = new NetworkModelGDAProxy("NetworkModelGDAEndpoint");
                gdaQueryProxy.Open();

                return gdaQueryProxy;
            }
        }

        #region GDAQueryService

        public List<ResourceDescription> GetAllResources()
        {
            string message = "GetAllResources method started";
            Console.WriteLine(message);
            CommonTrace.WriteTrace(CommonTrace.TraceInfo, message);

            List<ResourceDescription> resources = new List<ResourceDescription>();

            List<ModelCode> properties = new List<ModelCode>();

            int iteratorId = 0;
            int numberOfResources = 1000;

            try
            {
                foreach (DMSType type in Enum.GetValues(typeof(DMSType)))
                {
                    if (type == DMSType.MASK_TYPE)
                    {
                        continue;
                    }
                    
                    properties = modelResourcesDesc.GetAllPropertyIds(type);

                    iteratorId = GdaQueryProxy.GetExtentValues(modelResourcesDesc.GetModelCodeFromType(type), properties);
                    int count = GdaQueryProxy.IteratorResourcesLeft(iteratorId);

                    while (count > 0)
                    {
                        List<ResourceDescription> rds = GdaQueryProxy.IteratorNext(numberOfResources, iteratorId);

                        for (int i = 0; i < rds.Count; i++)
                        {
                            resources.Add(rds[i]);
                        }

                        count = GdaQueryProxy.IteratorResourcesLeft(iteratorId);
                    }

                    bool ok = GdaQueryProxy.IteratorClose(iteratorId);
                }


                message = "GetAllResources method successfully ended";
                Console.WriteLine(message);
                CommonTrace.WriteTrace(CommonTrace.TraceInfo, message);
            }

            catch (Exception e)
            {
                message = $"GetAllResources method failed: {e.Message}";
                Console.WriteLine(message);
                CommonTrace.WriteTrace(CommonTrace.TraceError, message);

                throw;
            }

            return resources;
        }

        public ResourceDescription GetResource(long resourceId)
        {
            string message = "GetResource method started";
            Console.WriteLine(message);
            CommonTrace.WriteTrace(CommonTrace.TraceInfo, message);

            ResourceDescription resource = null;

            try
            {
                short type = ModelCodeHelper.ExtractTypeFromGlobalId(resourceId);
                List<ModelCode> properties = modelResourcesDesc.GetAllPropertyIds((DMSType)type);

                resource = GdaQueryProxy.GetValues(resourceId, properties);

                message = "GetResource method successfully ended";
                Console.WriteLine(message);
                CommonTrace.WriteTrace(CommonTrace.TraceInfo, message);
            }
            catch (Exception e)
            {
                message = $"GetResource method failed: {e.Message}";
                Console.WriteLine(message);
                CommonTrace.WriteTrace(CommonTrace.TraceError, message);

                throw;
            }

            return resource;
        }

        public List<ResourceDescription> GetResourcesOfType(DMSType type)
        {
            string message = "GetResourcesOfType method started";
            Console.WriteLine(message);
            CommonTrace.WriteTrace(CommonTrace.TraceInfo, message);

            List<ResourceDescription> resources = new List<ResourceDescription>();

            List<ModelCode> properties = new List<ModelCode>();

            int iteratorId = 0;
            int numberOfResources = 1000;

            try
            {
                properties = modelResourcesDesc.GetAllPropertyIds(type);

                iteratorId = GdaQueryProxy.GetExtentValues(modelResourcesDesc.GetModelCodeFromType(type), properties);
                int count = GdaQueryProxy.IteratorResourcesLeft(iteratorId);

                while (count > 0)
                {
                    List<ResourceDescription> rds = GdaQueryProxy.IteratorNext(numberOfResources, iteratorId);

                    for (int i = 0; i < rds.Count; i++)
                    {
                        resources.Add(rds[i]);
                    }

                    count = GdaQueryProxy.IteratorResourcesLeft(iteratorId);
                }

                GdaQueryProxy.IteratorClose(iteratorId);

                message = "GetResourcesOfType method successfully ended";
                Console.WriteLine(message);
                CommonTrace.WriteTrace(CommonTrace.TraceInfo, message);
            }

            catch (Exception e)
            {
                message = $"GetResourcesOfType method failed: {e.Message}";
                Console.WriteLine(message);
                CommonTrace.WriteTrace(CommonTrace.TraceError, message);

                throw;
            }

            return resources;
        }

        public List<DMSType> GetDMSTypes()
        {
            List<DMSType> types = modelResourcesDesc.AllDMSTypes.ToList();
            types.Remove(DMSType.MASK_TYPE);
            return types;
        }

        #endregion GDAQueryService

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
