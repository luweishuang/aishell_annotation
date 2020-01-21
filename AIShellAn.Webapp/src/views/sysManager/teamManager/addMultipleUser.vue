<template>
  <div>
    <Table :height="600" :columns="userItemsColumns" :data="userItems" size="large">
      <template slot-scope="{ index,row }" slot="userName">
        <Input v-model="saveUserItems[index].userName" autocomplete="off"></Input>
      </template>
      <template slot-scope="{index, row }" slot="name">
        <Input v-model="saveUserItems[index].name" autocomplete="off"></Input>
      </template>
      <template slot-scope="{index, row }" slot="sex">
        <RadioGroup v-model="saveUserItems[index].sex">
          <Radio label="男"></Radio>
          <Radio label="女"></Radio>
        </RadioGroup>
      </template>
      <template slot-scope="{index, row }" slot="password">
        <Input v-model="saveUserItems[index].password" autocomplete="off"></Input>
      </template>
      <template slot-scope="{ index,row }" slot="phone">
        <Input v-model="saveUserItems[index].phone" autocomplete="off"></Input>
      </template>
      <template slot-scope="{index, row }" slot="role">
        <Select v-model="saveUserItems[index].role"  style="width:150px" clearable>
          <Option v-for="item in roleList" :value="item.name" :key="item.name">{{ item.name }}</Option>
        </Select>
      </template>
    </Table>
    <Row class="margin-top-10" type="flex" justify="center" align="middle">
      <Col span="3">
        <Button style="width:100%;" type="primary" @click="save">保存</Button>
      </Col>
    </Row>
  </div>
</template>
<script>
export default {
  props: ["userItem"],
  data() {
    return {
      userItems: [],
      saveUserItems: [],
      userItemsColumns: [
        {
          type: "index",
          width: 60,
          align: "center"
        },
        {
          title: "*用户名",
          key: "userName",
          slot: "userName",
          align: "center"
        },
        {
          title: "*姓名",
          key: "name",
          align: "center",
          slot: "name"
        },
        {
          title: "性别",
          key: "sex",
          slot: "sex",
          align: "center"
        },
        {
          title: "*密码",
          key: "password",
          slot: "password",
          align: "center"
        },
        {
          title: "手机",
          key: "phone",
          slot: "phone",
          align: "center"
        },
        {
          title: "*角色",
          key: "role",
          slot: "role",
          align: "center",
          width: 200
        }
      ],
      roleList: [],
      selectRole: []
    };
  },
  methods: {
    save() {
      let _this = this;

      for(let i=0;i<this.saveUserItems.length;i++){
      
        if(this.saveUserItems[i].role){
           let r=  this.saveUserItems[i].role;
         this.saveUserItems[i].role=[];
         this.saveUserItems[i].role.push(r);
        }
      }

      saveMultipleUser(this.saveUserItems).then(res => {
           _this.$emit('on-Saved');
      }).catch(err=>{
           _this.$Message.error
                ({
                  content: err.response.data
                });
      });
    }
  },
  mounted() {
    let _this = this;
    getRoleList().then(res => {
      _this.roleList = res.data.data;
    });

    var userItem = {
      userName: "",
      name: "",
      sex: "男"
    };

    for (let i = 0; i < 10; i++) {
      var newObj = Object.assign({}, userItem);
      this.userItems.push(newObj);
    }
    this.saveUserItems = JSON.parse(JSON.stringify(this.userItems));
  }
};
</script>
