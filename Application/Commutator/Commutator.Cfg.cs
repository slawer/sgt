using System;
using System.Xml;

using WCF;
using WCF.WCF_Client;

namespace SGT
{
    /// <summary>
    /// Реализует коммутатор, который отвечает за 
    /// соединение, прием и передачу данных от devMan
    /// </summary>
    public partial class Commutator
    {
        // ------------------------------ сохранение/загрузка ------------------------------

        /// <summary>
        /// Корневой узел коммутатора
        /// </summary>
        public const string RootName = "commutator";

        /// <summary>
        /// Корневой узел списка параметров
        /// </summary>
        protected const string ParametersName = "parameters";

        /// <summary>
        /// узел в котором сохраняется информация об uri
        /// </summary>
        protected const string UriName = "uri";

        /// <summary>
        /// Узел в котором сохраняется порт подключения к серверу данных
        /// </summary>
        protected const string portName = "port";

        /// <summary>
        /// Сохранить конфигурацию приложения
        /// </summary>
        /// <param name="doc">Документ в который осуществляется сохранение конфигурации</param>
        /// <param name="Root">Корневой узел в который необходимо сохранить конфигурацию коммутатора</param>
        public void Save(XmlDocument doc, XmlNode Root)
        {
            try
            {
                if (Root != null)
                {
                    XmlNode commutatorNode = doc.CreateElement(RootName);
                    
                    XmlNode devManUriNode = doc.CreateElement(UriName);
                    XmlNode devManPortNode = doc.CreateElement(portName);

                    devManUriNode.InnerText = DevManClient.DevManUri.OriginalString;
                    devManPortNode.InnerText = client.Client.Port.ToString();

                    commutatorNode.AppendChild(devManUriNode);
                    commutatorNode.AppendChild(devManPortNode);
                    
                    SaveParameters(doc, commutatorNode);                    

                    Root.AppendChild(commutatorNode);
                }
            }
            catch { }
        }

        /// <summary>
        /// Сохранить параметры
        /// </summary>
        /// <param name="doc">Документ в который осуществляется сохранение</param>
        /// <param name="root">Корневой узел, в который сохранять параметры</param>
        protected void SaveParameters(XmlDocument doc, XmlNode root)
        {
            if (p_slim.TryEnterWriteLock(500))
            {
                try
                {
                    if (doc != null && root != null)
                    {
                        XmlNode p_root = doc.CreateElement(ParametersName);
                        if (p_root != null)
                        {
                            foreach (Parameter parameter in parameters)
                            {
                                if (parameter != null)
                                {
                                    XmlNode p_node = parameter.Save(doc);
                                    if (p_node != null)
                                    {
                                        p_root.AppendChild(p_node);
                                    }
                                }
                            }

                            root.AppendChild(p_root);
                        }
                    }
                }
                catch { }
                finally
                {
                    p_slim.ExitWriteLock();
                }
            }
        }

        /// <summary>
        /// Загрузить конфигурацию приложения
        /// </summary>
        /// <param name="Root">Корневой узел, в котором сохранена конфигурация коммутатора</param>
        public void Load(XmlNode Root)
        {
            if (Root != null && Root.HasChildNodes)
            {
                foreach (XmlNode Child in Root.ChildNodes)
                {
                    switch (Child.Name)
                    {
                        case UriName:

                            try
                            {
                                DevManClient.DevManUri = new Uri(Child.InnerText);
                            }
                            catch { }
                            break;

                        case portName:

                            try
                            {
                            }
                            catch { }
                            break;

                        case ParametersName:

                            try
                            {
                                LoadParameters(Child);
                            }
                            catch { }
                            break;

                        default:
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Загрузить параметры коммутатора
        /// </summary>
        /// <param name="p_root">Корневой узел списка параметров</param>
        protected void LoadParameters(XmlNode p_root)
        {
            if (p_slim.TryEnterWriteLock(300))
            {
                try
                {
                    if (p_root != null && p_root.HasChildNodes)
                    {
                        int free_number = 0;
                        foreach (XmlNode Child in p_root)
                        {
                            switch (Child.Name)
                            {
                                case Parameter.parameterRoot:

                                    try
                                    {
                                        parameters[free_number++].Load(Child);
                                    }
                                    catch { }
                                    break;

                                default:
                                    break;
                            }
                        }
                    }
                }
                finally
                {
                    p_slim.ExitWriteLock();
                }
            }
        }

        // ---------------------------------------------------------------------------------
    }
}