using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using ProtoBuf;

namespace ET
{
    [ProtoContract]
    [Config]
    public partial class FuluConfigCategory : ProtoObject, IMerge
    {
        public static FuluConfigCategory Instance;
		
        [ProtoIgnore]
        [BsonIgnore]
        private Dictionary<int, FuluConfig> dict = new Dictionary<int, FuluConfig>();
		
        [BsonElement]
        [ProtoMember(1)]
        private List<FuluConfig> list = new List<FuluConfig>();
		
        public FuluConfigCategory()
        {
            Instance = this;
        }
        
        public void Merge(object o)
        {
            FuluConfigCategory s = o as FuluConfigCategory;
            this.list.AddRange(s.list);
        }
		
        public override void EndInit()
        {
            foreach (FuluConfig config in list)
            {
                config.EndInit();
                this.dict.Add(config.Id, config);
            }            
            this.AfterEndInit();
        }
		
        public FuluConfig Get(int id)
        {
            this.dict.TryGetValue(id, out FuluConfig item);

            if (item == null)
            {
                throw new Exception($"配置找不到，配置表名: {nameof (FuluConfig)}，配置id: {id}");
            }

            return item;
        }
		
        public bool Contain(int id)
        {
            return this.dict.ContainsKey(id);
        }

        public Dictionary<int, FuluConfig> GetAll()
        {
            return this.dict;
        }

        public FuluConfig GetOne()
        {
            if (this.dict == null || this.dict.Count <= 0)
            {
                return null;
            }
            return this.dict.Values.GetEnumerator().Current;
        }
    }

    [ProtoContract]
	public partial class FuluConfig: ProtoObject, IConfig
	{
		/// <summary>Id</summary>
		[ProtoMember(1)]
		public int Id { get; set; }
		/// <summary>Name</summary>
		[ProtoMember(2)]
		public string Name { get; set; }
		/// <summary>描述</summary>
		[ProtoMember(3)]
		public string Desc { get; set; }

	}
}
