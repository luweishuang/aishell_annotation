<template>
  <div>
    <Row type="flex" justify="end">
      <Col span="2" ><Button type="primary"  icon="md-add" @click="addUser">添加用户</Button></Col>
    </Row>
    <Card class="margin-top-10">
      <Table
        ref="userTable"
        :data="userTable"
        :columns="userTableColumns"
        no-data-text="暂无数据"
        height="600"
        border
        :loading="userTableLoading"
      >
        <template slot-scope="{ row }" slot="userName">
          <strong>{{ row.userName }}</strong>
        </template>
        <template slot-scope="{ row }" slot="role">
          <Tag v-for="(item,index) in row.role.split(',')" :key="index" color="primary">{{ item }}</Tag>
        </template>
        <template slot-scope="{ row, index }" slot="action">
          <ButtonGroup size="small">
            <!-- <Button title="登录" type="primary" @click="login(row)">登录</Button> -->
            <Button title="编辑" type="primary" @click="handleEdit(row)">编辑</Button>
            <Button
              title="删除"
              type="primary"
              v-if="(row.userName!='admin')"
              @click="handleDelete(row)"
            >删除</Button>
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
      <Modal
        v-model="addUserModal"
        :title="addUserTitle"
        :mask="true"
        :mask-closable="false"
        footer-hide
      >
        <add-or-edit-user v-on:on-Saved="saveUser()" :userItem="userItem"></add-or-edit-user>
      </Modal>
    </div>
  </div>
</template>
<script>
import addOrEditUser from "./addOrEditUser";
import { mapActions } from "vuex";
export default {
  components: {
    addOrEditUser
  },
  data() {
    return {
      userTableLoading: true,
      userItem: {
        id: "",
        userName: "",
        password: "",
        passwordConfirm: "",
        realName: "",
        sex: "",
        email: "",
        role: ""
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
          title: "创建者",
          key: "managerName",
          align: "center"
        },
        {
          title: "用户名",
          key: "userName",
          slot: "userName",
          align: "center"
        },
        {
          title: "姓名",
          key: "realName",
          align: "center"
        },
        {
          title: "性别",
          key: "sex",

          align: "center"
        },
        {
          title: "邮箱",
          key: "email",

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
          width: 200,
          slot: "action"
        }
      ],
      addUserModal: false
    };
  },
  methods: {
    ...mapActions(["getUserInfo", "handleLogin"]),
    handleSearch() {
      this.getData();
    },
    addUser() {
      let _this = this;
      _this.userId = "";
      this.userItem.id = "";

      _this.addUserTitle = "添加用户";
      _this.addUserModal = true;
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
        for (let i in res.data) {
          _this.userItem[i] = res.data[i];
        }
        this.addUserTitle = "编辑用户";
        this.addUserModal = true;
      });
    },
    getData() {
      let _this = this;
      this.userTableLoading = true;
      this.$api.user.getUserList(this.query).then(res => {
        if(res.success)
        {
          _this.userTable = res.data.list;
          _this.userTableCount = res.data.count;
          _this.userTableLoading = false;
        }
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
      this.getData();
    }
  },
  mounted() {
    this.getData();
  }
};
</script>
