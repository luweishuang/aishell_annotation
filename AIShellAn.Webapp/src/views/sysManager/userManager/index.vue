<template>
    <div>
        <managerPage :page="page" >
            
            <template  slot="item">
               
            </template>
        </managerPage>
    </div>
</template>
<script>
import managerPage from '@/components/manager-page'
export default {
    components: {
        managerPage
    },
    data(){
        return {
            data:[],
            columns:[
                {
                    title: "内容",
                    slot: "content",
                    align: "left"
                }
            ],
            page:{
                total:1,
                size:2,
                current:3
            }
        }
    }
}
</script>
