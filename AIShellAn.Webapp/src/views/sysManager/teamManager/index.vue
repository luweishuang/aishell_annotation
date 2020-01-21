<template>
  <div>
    <div class="search-con">团队名：
      <Input clearable placeholder="输入团队名" class="search-input" v-model="query.name"/>
      <Button @click="handleSearch" class="search-btn" type="primary" style="margin-left:8px;">
        <Icon type="search"/>&nbsp;&nbsp;搜索
      </Button>
      <Button type="info" class="myBtnRight" icon="md-add" style="margin-left:8px;" @click="addTeamBtn" >添加团队</Button>
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
        <Button  @click="showMember(row)" >成员</Button>
        <Button  @click="deleteTeam(row)" >删除</Button>
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
    <Modal v-model="addteamModal" title="添加新团队" :mask="true" :mask-closable="false" @on-ok="handleSave">
      <add-teamcom  ref="addTemCom"></add-teamcom>
    </Modal>
      <Modal v-model="memberModal"  title="团队成员" :mask="true" :mask-closable="false"  width="850">
        <teamMember ref="teamMember" ></teamMember>
       <div slot="footer"></div>
    </Modal>
  </div>
</template>
<script>
 import { Spin } from 'iview'
import addTeamcom from "./addTeam.vue";
import teamMember from "./teamMember.vue"
import { saveTeam,getTeamList,deleteTeam } from "@/api/team";
export default {
  name:'taskManager',
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
        {
          title: "操作",
          align: "center",
          slot: "action"
        }
      ],
      addteamModal: false,
      memberModal:false,
    };
  },
  methods: {
    handleSearch() {
       this.getData();
    },
    addTeamBtn() {
      let _this = this;
      _this.addteamModal = true;
    },
    handleSave(){
      let _this=this;
      let data=this.$refs.addTemCom.teamItem;
      if(data.teamName){
          saveTeam(data).then(res=>{
         _this.$Message.success({
           content:res.data.message
         });
         _this.getData();
      }).catch(err=>{
        _this.$Message.error
              ({
                content: err.response.data.message
              });
      })
      }else{
         _this.$Message.error
              ("团队名称不能为空！");
      }
    },
  
    getData(){
      let _this=this;
      getTeamList(this.query).then(res=>{
        _this.teamTable=res.data.data;
        _this.teamTableCount=res.data.count;
      })
    },
    deleteTeam(row){
      let _this=this;
      this.$Modal.confirm({
        title:"系统提示",
        content:"是否删除该团队？",
        onOk:()=>{
          deleteTeam(row.id).then(res=>{
               _this.getData();
          });
        }
      });
    },
    showMember(row){
      this.memberModal=true;
        this.$refs.teamMember.getMemberData(row.id);
    }
  },
  mounted() {
    this.getData();
  }
};
</script>
