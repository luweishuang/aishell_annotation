<template>
  <div>
    <managerPage
      ref="userManagerPage"
      :page="page"
      :loading="dataLoading"
      :data="data"
      :columns="columns"
      :filterList="filterList"
      @search="getData"
      @add="handleAdd"
      @bulk-delete="handleBulkDelete"
    >
      <template slot-scope="{ row }" slot="userName">
        <strong>{{row.userName}}</strong>
      </template>
      <template slot-scope="{ row }" slot="sex">
        <span v-if="row.sex==0">男</span>
        <span v-else>女</span>
      </template>
      <template slot-scope="{ row }" slot="role">
        <Tag v-for="(item,index) in row.role.split(',')" :key="index" color="primary">{{ item }}</Tag>
      </template>
      <template slot-scope="{ row }" slot="action">
        <Button type="text" @click="handleUpdate(row.id)">编辑</Button>|
        <Button type="text" @click="handleDelete(row.id)">删除</Button>
      </template>
    </managerPage>

    <Modal
     
      v-model="addUpdateModal"
      :title="addUpdateTitle"
      :mask="true"
      :mask-closable="false"
      footer-hide
    >
      <add-update  ref="addUpdate" @on-saved="handleSavedUser()"></add-update>
    </Modal>
  </div>
</template>
<script>
import managerPage from "@/components/manager-page";
import addUpdate from "./addUpdate.vue";
export default {
  components: {
    managerPage,
    addUpdate
  },
  data() {
    return {
      dataLoading: false,
      data: [],
      columns: [
        {
          type: "selection",
          width: 60,
          align: "center"
        },
        {
          title: "管理员",
          key: "managerName",
          align: "center",
          minWidth: 100
        },
        {
          title: "用户名",
          key: "userName",
          slot: "userName",
          align: "center",
          minWidth: 100
        },
        {
          title: "姓名",
          key: "realName",
          align: "center",
          minWidth: 100
        },
        {
          title: "性别",
          slot: "sex",
          align: "center",
          minWidth: 100
        },
        // {
        //   title: "籍贯",
        //   key: "nativePlace",
        //   align: "center",
        //   minWidth: 100
        // },
        // {
        //   title: "年龄",
        //   key: "age",
        //   align: "center",
        //   minWidth: 100
        // },
        // {
        //   title: "邮箱",
        //   key: "email",
        //   align: "center",
        //   minWidth: 100
        // },
        // {
        //   title: "手机",
        //   key: "phone",
        //   align: "center",
        //   minWidth: 100
        // },
        {
          title: "角色",
          key: "role",
          align: "center",
          slot: "role",
          minWidth: 100
        },
        {
          title: "最后登录时间",
          key: "lastLoginTime",
          align: "center",
          minWidth: 150
        },
        {
          title: "最后登录IP",
          key: "lastLoginIP",
          align: "center",
          minWidth: 150
        },
        {
          title: "操作",
          align: "center",
          slot: "action",
          minWidth: 170
        }
      ],
      page: {
        total: 1,
        size: 20,
        current: 1
      },
      filterList: [
        {
          name: "用户名称",
          fieldName: "userName",
          type: "input"
        },
        {
          name: "姓名",
          fieldName: "name",
          type: "input"
        },
        {
          name: "性别",
          fieldName: "sex",
          type: "select",
          props: {
            selectData: [
              {
                label: "男",
                value: 0
              },
              {
                label: "女",
                value: 1
              }
            ]
          }
        },
        {
          name: "最后登录时间",
          fieldName: "lastLoginTime",
          type: "daterange",
          props: {}
        },
        {
          name: "角色",
          fieldName: "role",
          type: "select",
          props: {
            selectData: [
              {
                label: "团队管理员",
                value: "团队管理员"
              },
              {
                label: "标注人员",
                value: "标注人员"
              },
              {
                label: "质检人员",
                value: "质检人员"
              },
              {
                label: "项目经理",
                value: "项目经理"
              }
            ]
          }
        }
      ],
      addUpdateModal: false,
      addUpdateTitle: ""
    };
  },
  watch:{
    addUpdateModal(value){
      if(value){
        this.$refs.addUpdate.clearData();
      }
    }
  },
  methods: {
    getData() {
      let _this = this;
      _this.dataLoading = true;
      let filter = _this.$refs.userManagerPage.getFilter();
      _this.$api.user
        .getUserList({
          userName: filter.userName,
          name: filter.name,
          sex: filter.sex,
          lastLoginTime:  filter.lastLoginTime? filter.lastLoginTime[0]:null,
          role: filter.role,
          page: _this.page.current,
          size: _this.page.size
        })
        .then(res => {
          if (res.success) {
            _this.data = res.data.list;
            _this.page.total = res.data.count;
            _this.dataLoading = false;
          }
        });
    },
    handleAdd() {
      this.addUpdateTitle = "添加用户";
      this.addUpdateModal = true;
    },
    handleUpdate(userId) {
      this.addUpdateTitle = "编辑用户";
      this.addUpdateModal = true;
    },
    handleDelete(userId) {
      let user = this.data.filter(x => x.id == userId)[0];
      this.$Modal.confirm({
        title: "系统提示",
        content: `是否要删除用户【${user.userName}】`,
        onOk: () => {}
      });
    },
    handleBulkDelete() {},
    handleSavedUser(){
      this.addUpdateModal=false;
      this.getData();
    }
  },
  mounted() {
    this.getData();
  }
};
</script>
