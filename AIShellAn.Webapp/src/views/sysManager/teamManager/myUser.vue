
<template>
  <div>
      <Button type="info" style="float:right;" icon="md-add" @click="addUser">添加用户</Button>
      <Button type="info" style="float:right;margin-right:8px;" icon="md-add" @click="addMultipleUser">批量添加用户</Button>
      <Button type="info" style="float:right;margin-right:8px;" icon="md-add" @click="downloadUserTemplate" >下载用户信息模板</Button>
      <Button type="info" style="float:right;margin-right:8px;" icon="md-add" @click="uploadUserTemplate">上传用户信息</Button>
   
     
    <!-- <div class="search-con">
        <span>用户名：</span>
        <Input clearable placeholder="输入用户名" class="search-input" v-model="query.userName"/>管理者：
        <Input clearable placeholder="输入用户名" class="search-input" v-model="query.managerName"/>

        <Button @click="handleSearch" class="search-btn" type="primary">
          <Icon type="search"/>&nbsp;&nbsp;搜索
        </Button>
    </div>-->
    <Card class="margin-top-10">
      <Table
        ref="userTable"
        :data="userTable"
        :columns="userTableColumns"
        no-data-text="暂无数据"
        height="600"
        border
      >
        <template slot-scope="{ row }" slot="userName">
          <strong>{{ row.userName }}</strong>
        </template>
        <template slot-scope="{ row }" slot="role">
          <Tag v-for="item in row.role" color="primary">{{ item }}</Tag>
        </template>
        <template slot-scope="{ row, index }" slot="action">
          <ButtonGroup>
            <Button title="编辑" @click="handleEdit(row)">编辑</Button>
            <Button title="删除" v-if="(row.userName!='admin')" @click="handleDelete(row)">删除</Button>
          </ButtonGroup>
        </template>
      </Table>
      <Page
        :total="userTableCount"
        :page-size="query.size"
        :current="query.page"
        class="my-page"
        show-elevator
        
        show-total
        @on-change="changePage"
        @on-page-size-change="changePageSize"
      />
    </Card>
    <div>

      <Modal    v-model="addMultipleUserModal"
        title="批量添加用户"
        :mask="true"
        :mask-closable="false"
        footer-hide
        :width="1000">
      <add-multiple-user v-on:on-Saved="saveUser()" > </add-multiple-user>
      </Modal>

      <Modal
        v-model="addUserModal"
        :title="addUserTitle"
        :mask="true"
        :mask-closable="false"
        footer-hide
      >
        <add-or-edit-user v-on:on-Saved="saveUser()" :userItem="userItem"></add-or-edit-user>
      </Modal>
       <Modal v-model="uploadModal" title="上传文档" :mask="true" :mask-closable="false" >
        <upload-user ref="uploadModal"  @save="getData"></upload-user>
      <div slot="footer"></div>
    </Modal>
    </div>
  </div>
</template>
<script>
import addOrEditUser from "@/views/admin/user/addOrEditUser";
import addMultipleUser from "./addMultipleUser"
import queryBox from "@/views/components/queryBox";
import { getUserList, deleteUser, getUser } from "@/api/user";
import uploadUser from "./uploadUser";
export default {
  components: {
    addOrEditUser,
    addMultipleUser,
    queryBox,
    uploadUser
  },
  data() {
    return {
      uploadModal:false,
      dowloadUserTemplateUrl:window.ajaxUrl+"/api/File/DownLoadUserTemplate",
      showQueryBox: false,
      userItem: {
        id: "",
        userName: "",
        password: "",
        passwordConfirm: "",
        name: "",
        sex: "",
        email: "",
        role: []
      },
      query: {
        userName: "",
        page: 1,
        size: 20
      },
      userId: "",
      addUserTitle: "",
      userTable: [],
      userTableCount: 0,
      userTableColumns: [
        {
          title: "所在团队",
          key: "team",
          align: "center",
          width:200
        },
        {
          title: "用户名",
          key: "userName",
          slot: "userName",
          align: "center"
        },
        {
          title: "姓名",
          key: "name",
          align: "center"
        },
        {
          title: "性别",
          key: "sex",

          align: "center"
        },
        {
          title: "手机",
          key: "phone",
          align: "center"
        },
        {
          title: "角色",
          key: "role",
          align: "center",
          slot: "role"
        },
        {
          title: "最后登录时间",
          key: "lastLoginTime",
          align: "center"
        },
        {
          title: "操作",
          align: "center",
          slot: "action"
        }
      ],
      addUserModal: false,
      addMultipleUserModal:false
    };
  },
  methods: {
    handleSearch() {
      this.getData();
    },
    addUser() {
      let _this = this;
      _this.userId = "";
        this.userItem.id="";
        this.userItem.userName="";
        this.userItem.name="";
      _this.addUserTitle = "添加用户";
      _this.addUserModal = true;
    },
    addMultipleUser(){
        let _this = this;
        _this.addMultipleUserModal = true;
    },
    handleDelete(row) {
      let _this = this;
      this.$Modal.confirm({
        title: "系统提示",
        content: "是否删除该用户？",
        onOk: () => {
          deleteUser({ value: row.id }).then(res => {
            _this.$Message.success({
              content: res.data.message
            });
            _this.getData();
          });
        }
      });
    },
    handleEdit(row) {
      let _this = this;
      getUser(row.id).then(res => {
        console.log(res);
        for (let i in res.data) {
          _this.userItem[i] = res.data[i];
        }
        this.addUserTitle = "编辑用户";
        this.addUserModal = true;
      });
    },
    getData() {
      let _this = this;
      getUserList(this.query).then(res => {
        _this.userTable = res.data.data;
        _this.userTableCount = res.data.count;
      });
    },
    changePage(page) {
      this.query.page = page;
      this.getData();
    },
    changePageSize(pageSize) {
      this.query.size = pageSize;
      this.getData();
    },
    saveUser() {
      this.addUserModal = false;
      this.addMultipleUserModal=false;
      this.getData();
    },
    downloadUserTemplate(){
      window.open(this.dowloadUserTemplateUrl);
    },
    uploadUserTemplate(){
      this.uploadModal=true;
    }
  },
  mounted() {
    this.getData();
  }
};
</script>
