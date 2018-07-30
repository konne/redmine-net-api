﻿/*
   Copyright 2011 - 2018 Adrian Popescu.

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



using Redmine.Net.Api.Extensions;
using Redmine.Net.Api.Internals;
using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Redmine.Net.Api.Types
{
    /// <summary>
    /// 
    /// </summary>
    [XmlRoot(RedmineKeys.FILE)]
    public class File : Identifiable<File>, IEquatable<File>, IXmlSerializable
    {

        /// <summary>
        /// 
        /// </summary>
        [XmlElement(RedmineKeys.FILENAME)]
        public string Filename { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlElement(RedmineKeys.FILESIZE)]
        public int Filesize { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlElement(RedmineKeys.CONTENT_TYPE)]
        public string ContentType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlElement(RedmineKeys.DESCRIPTION)]
        public string Description { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlElement(RedmineKeys.CONTENT_URL)]
        public string ContentUrl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlElement(RedmineKeys.AUTHOR)]
        public IdentifiableName Author { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlElement(RedmineKeys.CREATED_ON)]
        public DateTime? CreatedOn { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlElement(RedmineKeys.VERSION)]
        public IdentifiableName Version { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlElement(RedmineKeys.DIGEST)]
        public string Digest { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlElement(RedmineKeys.DOWNLOADS)]
        public int Downloads { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlElement(RedmineKeys.TOKEN)]
        public string Token { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(File other)
        {
            if (other == null) return false;
            return (Id == other.Id 
                && Filename == other.Filename 
                && Filesize == other.Filesize 
                && Description == other.Description
                && ContentType == other.ContentType 
                && ContentUrl == other.ContentUrl
                && Author ==other.Author
                && CreatedOn == other.CreatedOn
                && Version == other.Version
                && Digest == other.Digest
                && Downloads == other.Downloads
                && Token == other.Token
                );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            var hashCode = base.GetHashCode();

            hashCode = HashCodeHelper.GetHashCode(Filename, hashCode);
            hashCode = HashCodeHelper.GetHashCode(Filesize, hashCode);
            hashCode = HashCodeHelper.GetHashCode(ContentType, hashCode);
            hashCode = HashCodeHelper.GetHashCode(Description, hashCode);
            hashCode = HashCodeHelper.GetHashCode(Author, hashCode);
            hashCode = HashCodeHelper.GetHashCode(ContentUrl, hashCode);

            hashCode = HashCodeHelper.GetHashCode(Author, hashCode);
            hashCode = HashCodeHelper.GetHashCode(CreatedOn, hashCode);
            hashCode = HashCodeHelper.GetHashCode(Version, hashCode);
            hashCode = HashCodeHelper.GetHashCode(Digest, hashCode);
            hashCode = HashCodeHelper.GetHashCode(Downloads, hashCode);

            return hashCode;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("[File: Id={0}, Name={1}]", Id, Filename);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals(obj as File);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public XmlSchema GetSchema()
        {
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        public void ReadXml(XmlReader reader)
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
                    case RedmineKeys.ID: Id = reader.ReadElementContentAsInt(); break;
                    case RedmineKeys.FILENAME: Filename = reader.ReadElementContentAsString(); break;
                    case RedmineKeys.FILESIZE: Filesize = reader.ReadElementContentAsInt(); break;
                    case RedmineKeys.CONTENT_TYPE: ContentType = reader.ReadElementContentAsString(); break;
                    case RedmineKeys.DESCRIPTION: Description = reader.ReadElementContentAsString(); break;
                    case RedmineKeys.CONTENT_URL: ContentUrl = reader.ReadElementContentAsString(); break;
                    case RedmineKeys.AUTHOR: Author = new IdentifiableName(reader); break;
                    case RedmineKeys.CREATED_ON:CreatedOn = reader.ReadElementContentAsNullableDateTime(); break;
                    case RedmineKeys.VERSION: Version = new IdentifiableName(reader); break;
                    case RedmineKeys.VERSION_ID: Version = new IdentifiableName() {Id = reader.ReadElementContentAsInt()}; break;
                    case RedmineKeys.DIGEST: Digest = reader.ReadElementContentAsString(); break;
                    case RedmineKeys.DOWNLOADS: Downloads = reader.ReadElementContentAsInt(); break;
                    case RedmineKeys.TOKEN:Token = reader.ReadElementContentAsString(); break;
                    default:
                        reader.Read();
                        break;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        public void WriteXml(XmlWriter writer)
        {
            writer.WriteElementString(RedmineKeys.TOKEN, Token);
            writer.WriteIdIfNotNull(Version, RedmineKeys.VERSION_ID);
            writer.WriteElementString(RedmineKeys.FILENAME, Filename);
            writer.WriteElementString(RedmineKeys.DESCRIPTION, Description);
        }
    }
}