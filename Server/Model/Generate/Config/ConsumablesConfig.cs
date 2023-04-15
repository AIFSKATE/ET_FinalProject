using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using ProtoBuf;

namespace ET
{
    [ProtoContract]
    [Config]
    public partial class ConsumablesConfigCategory : ProtoObject, IMerge
    {
        public static ConsumablesConfigCategory Instance;
		
        [ProtoIgnore]
        [BsonIgnore]
        private Dictionary<int, ConsumablesConfig> dict = new Dictionary<int, ConsumablesConfig>();
		
        [BsonElement]
        [ProtoMember(1)]
        private List<ConsumablesConfig> list = new List<ConsumablesConfig>();
		
        public ConsumablesConfigCategory()
        {
            Instance = this;
        }
        
        public void Merge(object o)
        {
            ConsumablesConfigCategory s = o as ConsumablesConfigCategory;
            this.list.AddRange(s.list);
        }
		
        public override void EndInit()
        {
            foreach (ConsumablesConfig config in list)
            {
                config.EndInit();
                this.dict.Add(config.Id, config);
            }            
            this.AfterEndInit();
        }
		
        public ConsumablesConfig Get(int id)
        {
            this.dict.TryGetValue(id, out ConsumablesConfig item);

            if (item == null)
            {
                throw new Exception($"配置找不到，配置表名: {nameof (ConsumablesConfig)}，配置id: {id}");
            }

            return item;
        }
		
        public bool Contain(int id)
        {
            return this.dict.ContainsKey(id);
        }

        public Dictionary<int, ConsumablesConfig> GetAll()
        {
            return this.dict;
        }

        public ConsumablesConfig GetOne()
        {
            if (this.dict == null || this.dict.Count <= 0)
            {
                return null;
            }
            return this.dict.Values.GetEnumerator().Current;
        }
    }

    [ProtoContract]
	public partial class ConsumablesConfig: ProtoObject, IConfig
	{
		/// <summary>Id</summary>
		[ProtoMember(1)]
		public int Id { get; set; }
		/// <summary>名字</summary>
		[ProtoMember(2)]
		public string Name { get; set; }
		/// <summary>描述</summary>
		[ProtoMember(3)]
		public string Desc { get; set; }
		/// <summary>冷却时间</summary>
		[ProtoMember(4)]
		public float CoolTime { get; set; }

	}
}
