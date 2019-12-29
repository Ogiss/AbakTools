using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BAL.Business;
using AbakTools.Business;
using AbakTools.Towary;

namespace AbakTools.Handel.Forms
{
    public class KategorieTreeView : TreeView
    {
        #region Fields

        private BAL.Business.View dataContext;
        private bool fireAfterSelect;

        private Enova.Business.Old.DB.Web.KategoriaOld wybranaKategoria;

        #endregion

        #region properties

        public bool KategorieWeb;
        public bool KategorieEnova;

        public BAL.Business.View DataContext
        {
            get { return this.dataContext; }
            set
            {
                this.dataContext = value;
                this.loadKategorie();
            }
        }

        public Enova.Business.Old.DB.Web.KategoriaOld WybranaKategoria
        {
            get { return wybranaKategoria; }
        }

        #endregion

        #region Methods

        public KategorieTreeView()
        {
            //this.MinimumSize = new System.Drawing.Size(200, 200);
            this.HideSelection = false;
        }

        private void loadKategorie()
        {
            Nodes.Clear();

            if (KategorieWeb || KategorieEnova)
            {
                var rootCategory = this.calcRootCategory();
                if (rootCategory != null)
                {
                    this.Nodes.Add(new KategoriaTreeNode(rootCategory, "Wszystko"));
                    this.Nodes[0].Expand();
                }
            }
            this.fireAfterSelect = true;
        }

        private Enova.Business.Old.DB.Web.KategoriaOld calcRootCategory()
        {
            if (!KategorieWeb)
                return Enova.Business.Old.DB.Web.KategoriaOld.EnovaRoot;
            return Enova.Business.Old.DB.Web.KategoriaOld.Root;

        }

        protected override void OnAfterSelect(TreeViewEventArgs e)
        {
            if (fireAfterSelect)
            {
                if (this.SelectedNode != null)
                {
                    wybranaKategoria = ((KategoriaTreeNode)this.SelectedNode).Kategoria;
                }
                else
                    this.wybranaKategoria = null;
                base.OnAfterSelect(e);
            }
        }

        #endregion

        #region Nested Types

        public class KategoriaTreeNode : TreeNode
        {
            private Enova.Business.Old.DB.Web.KategoriaOld kategoria;

            public Enova.Business.Old.DB.Web.KategoriaOld Kategoria
            {
                get { return kategoria; }
            }

            public KategoriaTreeNode(Enova.Business.Old.DB.Web.KategoriaOld kategoria, string text)
                : base(text)
            {
                this.kategoria = kategoria;
                this.initChilds();
            }

            public KategoriaTreeNode(Enova.Business.Old.DB.Web.KategoriaOld kategoria) : this(kategoria, kategoria.Nazwa) { }

            private void initChilds()
            {
                foreach (var child in kategoria.Podkategorie.Where(r => r.deleted == false && r.Synchronizacja != (byte)Enova.Business.Old.Types.RowSynchronizeOld.NotsynchronizedDelete).OrderBy(r => r.Nazwa))
                {
                    this.Nodes.Add(new KategoriaTreeNode(child));
                }
            }
        }

        #endregion

    }
}
