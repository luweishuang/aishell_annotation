<template>
    <Form :model="teamItem" :label-width="100" >
    <FormItem label="团队名称:" required>
      <Input v-model="teamItem.teamName" autocomplete="off" placeholder="输入团队名称..."></Input>
    </FormItem>
    <FormItem label="团队介绍:" required>
      <Input  autocomplete="off" v-model="teamItem.describe" placeholder="输入介绍..."></Input>
    </FormItem>
     <FormItem label="团队成员:" >
         <Select v-model="teamItem.teamUser" multiple  clearable filterable>
        <Option v-for="item in users" :value="item.id" :key="item.id">{{ item.userName }}</Option>
      </Select>
    </FormItem>
  </Form>
</template>
<script>
import { getUserList } from "@/api/user";
export default {
    data(){
        return {
            teamItem:{
                teamName:'',
                describe:'',
                teamUser:[]
            },
            users:[],
        //     teamValidate:{
        //        teamname: [
        //   { type: "string", required: true, message: "团队名称不能为空" }
        // ],
        //  introduce: [
        //   { type: "string", required: true, message: "团队介绍不能为空" }
        // ],
        //     }
         }
    },
    methods:{
      
    },
    mounted(){
      let _this=this;
      getUserList({userName:'',page:1,size:9999}).then(res=>{
          _this.users=res.data.data;
      })
    }
}
</script>
