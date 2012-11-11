﻿/*
   Copyright 2011 - 2012 Adrian Popescu, Dorin Huzum.

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
*/

using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.Web.Script.Serialization;

namespace Redmine.Net.Api.Types
{
    /// <summary>
    /// Availability 1.0
    /// </summary>
    [XmlRoot("project")]
    public class Project : IdentifiableName, IEquatable<Project>, IJSon<Project>
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        [XmlElement("identifier")]
        public String Identifier { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        [XmlElement("description")]
        public String Description { get; set; }

        /// <summary>
        /// Gets or sets the parent.
        /// </summary>
        /// <value>The parent.</value>
        [XmlElement("parent")]
        public IdentifiableName Parent { get; set; }

        /// <summary>
        /// Gets or sets the home page.
        /// </summary>
        /// <value>The home page.</value>
        [XmlElement("homepage")]
        public String HomePage { get; set; }

        /// <summary>
        /// Gets or sets the created on.
        /// </summary>
        /// <value>The created on.</value>
        [XmlElement("created_on")]
        public DateTime? CreatedOn { get; set; }

        /// <summary>
        /// Gets or sets the updated on.
        /// </summary>
        /// <value>The updated on.</value>
        [XmlElement("updated_on")]
        public DateTime? UpdatedOn { get; set; }

        /// <summary>
        /// Gets or sets the trackers.
        /// </summary>
        /// <value>
        /// The trackers.
        /// </value>
        [XmlArray("trackers")]
        [XmlArrayItem("tracker")]
        public IList<ProjectTracker> Trackers { get; set; }

        /// <summary>
        /// Generates an object from its XML representation.
        /// </summary>
        /// <param name="reader">The <see cref="T:System.Xml.XmlReader"/> stream from which the object is deserialized.</param>
        public override void ReadXml(XmlReader reader)
        {
            reader.Read();
            while (!reader.EOF)
            {
                if (reader.IsEmptyElement && !reader.HasAttributes)
                {
                    reader.Read();
                    continue;
                }

                switch (reader.Name)
                {
                    case "id": Id = reader.ReadElementContentAsInt(); break;

                    case "name": Name = reader.ReadElementContentAsString(); break;

                    case "identifier": Identifier = reader.ReadElementContentAsString(); break;

                    case "description": Description = reader.ReadElementContentAsString(); break;

                    case "parent": Parent = new IdentifiableName(reader); break;

                    case "homepage": HomePage = reader.ReadElementContentAsString(); break;

                    case "created_on": CreatedOn = reader.ReadElementContentAsNullableDateTime(); break;

                    case "updated_on": UpdatedOn = reader.ReadElementContentAsNullableDateTime(); break;

                    case "trackers": Trackers = reader.ReadElementContentAsCollection<ProjectTracker>(); break;

                    default: reader.Read(); break;
                }
            }
        }

        public override void WriteXml(XmlWriter writer)
        {
            writer.WriteElementString("name", Name);
            writer.WriteElementString("identifier", Identifier);
            writer.WriteElementString("description", Description);
            if (Id == 0) return;
            writer.WriteIdIfNotNull(Parent, "parent_id");
            writer.WriteElementString("homepage", HomePage);
        }

        public bool Equals(Project other)
        {
            if (other == null) return false;
            return (Id == other.Id && Identifier == other.Identifier);
        }

        public IEnumerable<Type> SupportedTypes
        {
            get { return new List<Type>(new Type[] { typeof(ChangeSet) }); }
        }

        public IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
        {
            return null;
        }

        public Project Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
        {
            return null;
        }

    }

    public interface IJSon<T>
    {
        IEnumerable<Type> SupportedTypes { get; }
        IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer);
        T Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer);
    }
}