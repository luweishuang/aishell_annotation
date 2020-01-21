<template>
  <div>
    <Row>
      <Select
        v-model="selectUser"
        clearable
        filterable
        remote
        :remote-method="remoteUser"
        :loading="loading1"
        style="width:200px;margin-right:5px;"
      >
        <Option
          v-for="(option, index) in userList"
          :value="option.id"
          :key="index"
        >{{option.userName}}</Option>
      </Select>
      <Button type="success" icon="md-add" @click="addMember">添加成员</Button>
    </Row>
    <Row style="margin-top:10px;">
      <Table :data="memberList" :columns="memberColumns">
        
         <template slot-scope="{ row, index }" slot="manager">
            <span v-if="row.isManager==false">否</span>
             <span v-if="row.isManager==true">是</span>
         </template>
        <template slot-scope="{ row, index }" slot="action">
          <ButtonGroup>
            <Button @click="setManager(row)" v-if="row.isManager==false&&hasRole(['项目经理'])"  >设为管理员</Button>
             <Button @click="setManager(row)" v-if="row.isManager==true&&hasRole(['项目经理'])" >撤销管理员</Button>
            <Button @click="removeMember(row.id)" type="error">删除</Button>
          </ButtonGroup>
        </template>
      </Table>
    </Row>
  </div>
</template>
<script>
import { getUserList } from "@/api/user";
import { addMember, getMemberList, removeMember, setManager } from "@/api/team";
import { hasOneOf } from "@/libs/tools";
export default {
  data() {
    return {
      selectUser: "",
      loading1: false,
      userList: [],
      teamId: "",
      memberList: [],
      memberColumns: [
        {
          title: "用户名",
          key: "userName",
          align: "center"
        },
        {
          title: "姓名",
          key: "name",
          align: "center"
        },
        {
          title: "是否团队管理员",
          key: "isManager",
          align: "center",
          slot:'manager'
        },
        {
          title: "操作",
          key: "action",
          align: "center",
          slot: "action",
          width:200
        }
      ]
    };
  },
  methods: {
       hasRole(roles) {
      return hasOneOf(this.$store.state.user.role, roles);
    },
    remoteUser(query) {
      let _this = this;
      if (query !== "") {
        this.loading1 = true;
        getUserList({ userName: query, page: 1, size: 999 }).then(res => {
          _this.userList = res.data.data;
          _this.loading1 = false;
        });
      }
      {
        _this.userList = [];
      }
    },
    addMember() {
      let _this = this;
      if (this.selectUser) {
        addMember({ userId: this.selectUser, teamId: this.teamId })
          .then(res => {
            _this.$Message.success({
              content: res.data.message
            });
            _this.getMemberData(this.teamId);
          })
          .catch(err => {
            _this.$Message.error({
              content: err.response.data.message
            });
          });
      }
    },
    getMemberData(id) {
      let _this = this;
      this.teamId = id;
      getMemberList(id).then(res => {
        console.log(res);
        _this.memberList = res.data.data;
      });
    },
    removeMember(id) {
      let _this = this;
      removeMember(id).then(res => {
        _this.$Message.success({
          content: res.data.message
        });
        _this.getMemberData(this.teamId);
      });
    },
    setManager(row) {
      let _this=this;
      setManager(row.id).then(res=>{
           _this.getMemberData(this.teamId);
      }) .catch(err => {
            _this.$Message.error({
              content: err.response.data.message
            });
          });
    }
  }
};
</script>
