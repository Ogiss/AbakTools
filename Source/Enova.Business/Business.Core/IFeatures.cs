using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enova.Business.Old.Core
{
    public interface IFeatures
    {
        bool SupportFeatures { get; }
        string FeaturesTableName { get; }
        void ApplyFeatureFilter(Enova.Business.Old.DB.FeatureDef featureDef, string value);
        void RemoveFeatureFilter();
    }
}
