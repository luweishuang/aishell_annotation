<template>
  <div>
    <div class="search-con">小组名：
      <Input clearable placeholder="输入小组名" class="search-input" v-model="query.name"/>
      <Button @click="handleSearch" class="search-btn" type="primary">
        <Icon type="search"/>&nbsp;&nbsp;搜索
      </Button>
    </div>
    <Card>
    <Table
      ref="groupTable"
      :data="groupTable"
      :columns="groupTableColumns"
      no-data-text="暂无数据"
      height="600"
      border
    >
      <slot name="loading" slot="loading"></slot>
      <template slot-scope="{ row }" slot="groupName">
         <a @click="showDetail(row)" class="underline" >{{row.groupName}}</a>
      </template>
    </Table>
    <Page
      :total="groupTableCount"
      :page-size="query.size"
      :current="query.page"
      class="my-page"
      show-elevator
    />
    </Card>
      
  </div>
</template>
<script>
 import { Spin } from 'iview'
import { getMyGroupList } from "@/api/team";
export default {
  components: {
    
  },
  data() {
    return {
      query: {
        name: "",
        page: 1,
        size: 20
      },
      groupTable: [],
      groupTableCount: 0,
      groupTableColumns: [
        {
          title: "团队名",
          key: "groupName",
          slot: "groupName",
          align: "center"
        },
        {
          title: "团队介绍",
          key: "describe",
          align: "center"
        },
          {
          title: "项目经理",
          key: "manager",
          align: "center"
        },
          {
          title: "团队管理员",
          key: "groupManager",
          align: "center",
          render:(h,params)=>{
            let usernames=params.row.groupManager.join(',');
            return h('span',usernames);
          }
        },
         {
          title: "成员数量",
          key: "count",
          align: "center"
        },
        {
          title: "操作",
          align: "center",
          slot: "action"
        }
      ],
      addgroupModal: false,
    };
  },
  methods: {
    handleSearch() {
       this.getData();
    },
  
    getData(){
      let _this=this;
      getMyGroupList(this.query).then(res=>{
        _this.groupTable=res.data.data;
        _this.groupTableCount=res.data.count;
      })
    },
    showDetail(row){
      console.log(row);
    }
  },
  mounted() {
    this.getData();
  }
};
</script>
