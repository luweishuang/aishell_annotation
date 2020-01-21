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
        <strong >{{ row.teamName }}</strong>
      </template>
      <template slot-scope="{ row, index }" slot="action">
        <ButtonGroup>
      
    </ButtonGroup>
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
  </div>
</template>
<script>
 import { Spin } from 'iview'
import addTeamcom from "./addTeam.vue";
import teamMember from "./teamMember.vue"
import { getMyBelongTeamList } from "@/api/team";
export default {
  components: {
    addTeamcom,
    teamMember
  },
  data() {
    return {
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
      ],
      addteamModal: false,
      memberModal:false,
    };
  },
  methods: {
    handleSearch() {
       this.getData();
    },
 
    getData(){
      let _this=this;
      getMyBelongTeamList(this.query).then(res=>{
        _this.teamTable=res.data.data;
        _this.teamTableCount=res.data.count;
      })
    },
  },
  mounted() {
    this.getData();
  }
};
</script>
