<template>
  <div>
    <div class="search-con">团队名：
      <Input clearable placeholder="输入团队名" class="search-input" v-model="query.name"/>
      <Button @click="handleSearch" class="search-btn" type="primary">
        <Icon type="search"/>&nbsp;&nbsp;搜索
      </Button>
    </div>
    <Card>
    <Table
      ref="teamTable"
      :data="teamTable"
      :columns="teamTableColumns"
      no-data-text="暂无数据"
      height="600"
      border
    >
      <slot name="loading" slot="loading"></slot>
      <template slot-scope="{ row }" slot="teamName">
         <a @click="showDetail(row)" class="underline" >{{row.teamName}}</a>
      </template>
    </Table>
    <Page
      :total="teamTableCount"
      :page-size="query.size"
      :current="query.page"
      class="my-page"
      show-elevator
    />
    </Card>
       <Modal v-model="memberModal"  title="团队成员" :mask="true" :mask-closable="false"  width="850">
        <teamMember ref="teamMember" ></teamMember>
       <div slot="footer"></div>
    </Modal>
  </div>
</template>
<script>
 import { Spin } from 'iview'
import addTeamcom from "./addTeam.vue";
import { getMyTeamList } from "@/api/team";
import teamMember from "./teamMember.vue"
export default {
  components: {
    addTeamcom,
    teamMember
  },
  data() {
    return {
      memberModal:false,
      query: {
        name: "",
        page: 1,
        size: 20
      },
      teamTable: [],
      teamTableCount: 0,
      teamTableColumns: [
        {
          title: "团队名",
          key: "teamName",
          slot: "teamName",
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
          key: "teamManager",
          align: "center",
          render:(h,params)=>{
            let usernames=params.row.teamManager.join(',');
            return h('span',usernames);
          }
        },
         {
          title: "成员数量",
          key: "count",
          align: "center"
        },
        // {
        //   title: "操作",
        //   align: "center",
        //   slot: "action"
        // }
      ],
      addteamModal: false,
    };
  },
  methods: {
    handleSearch() {
       this.getData();
    },
  
    getData(){
      let _this=this;
      getMyTeamList(this.query).then(res=>{
        _this.teamTable=res.data.data;
        _this.teamTableCount=res.data.count;
      })
    },
    showDetail(row){
       this.memberModal=true;
        this.$refs.teamMember.getMemberData(row.id);
    }
  },
  mounted() {
    this.getData();
  }
};
</script>
