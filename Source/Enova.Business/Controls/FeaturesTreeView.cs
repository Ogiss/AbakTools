using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Entity;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Windows.Forms;
using Enova.Business.Old.DB;

namespace Enova.Business.Old.Controls
{
    [ToolboxItem(false)]
    public class FeaturesTreeView : TreeView
    {
        private string tableName = null;
        private bool isLoaded = false;

        [Browsable(true)]
        public string TableName
        {
            get
            {
                return this.tableName;
            }
            set
            {
                this.tableName = value;
            }
        }

        public FeaturesTreeView() : base() { }
        public FeaturesTreeView(string tableName)
            : base()
        {
            this.TableName = tableName;
        }

        public void LoadFeatures(EnovaContext dc)
        {
            if (!isLoaded && dc != null && !string.IsNullOrEmpty(TableName))
            {
                this.Nodes.Add(new FeatureTreeNode(new FeatureDef() { Name = "Wszystko", ID = 0 }));
                var features = dc.FeatureDefs.Where(f => f.TableName == TableName && f.StrictDictionary == true && f.Group == true)
                    .OrderBy(f => f.Name).ToList();

                foreach (var f in features)
                {
                    this.Nodes.Add(new FeatureTreeNode(f));
                }
                isLoaded = true;
            }
        }


        internal class FeatureTreeNode : TreeNode
        {
            private FeatureDef featureDef;

            public FeatureTreeNode(FeatureDef featureDef)
                : base(featureDef.Name)
            {
                this.featureDef = featureDef;
                loadDictionary();
            }

            private void loadDictionary()
            {
                if (featureDef != null)
                {
                    var dictionarySet = featureDef.DictionarySet.OrderBy(d => d.Value).ToList();
                    foreach (var d in dictionarySet)
                    {
                        this.Nodes.Add(new DictionaryTreeNode(d, featureDef));
                    }
                }
            }

            public FeatureDef FeatureDef
            {
                get { return featureDef; }
            }

            public bool IsAll
            {
                get { return featureDef.ID == 0; }
            }
        }

        internal class DictionaryTreeNode : TreeNode
        {
            private Dictionary dictionary;
            private FeatureDef featureDef;

            public DictionaryTreeNode(Dictionary dic, FeatureDef fd)
                : base(dic.Value)
            {
                this.dictionary = dic;
                this.featureDef = fd;

                if (fd.IsTree)
                    loadSubDictionary();
                
            }

            private void loadSubDictionary()
            {
                var sd = dictionary.SubDictionary.OrderBy(d=>d.Value).ToList();
                foreach (var d in sd)
                {
                    this.Nodes.Add(new DictionaryTreeNode(d, featureDef));
                }
            }

            public Dictionary Dictionary
            {
                get { return dictionary; }
            }

            public FeatureDef FeatureDef
            {
                get { return featureDef; }
            }

            public string Value
            {
                get {
                    if (featureDef.IsTree)
                    {
                        return dictionary.Path;
                    }
                    return dictionary.Value;
                }
            }

        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // FeaturesTreeView
            // 
            this.FullRowSelect = true;
            this.HideSelection = false;
            this.LineColor = System.Drawing.Color.Black;
            this.ResumeLayout(false);

        }


    }
}
