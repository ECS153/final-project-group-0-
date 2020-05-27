<template>
    <div id= "credential-item"> 
        <input class="domain" :readonly="readMode" v-model="cred.domain">
        <input class="hint" :readonly="readMode" v-model="cred.hint">
        
        <h4 v-if="readMode" @click="edit" class="edit">Edit</h4> 
        <h4 v-else  @click="submit" class="submit">Submit</h4>
    </div>
</template>

<script>
export default {
    name: "CredentialItem",
    props: ["cred", "type"],
    data() {
        return {
            readMode: true,
        }
    },
    computed: {
        
    },
    methods: {
        
        edit: function() {
            this.readMode = false;
        },
        submit: function() {
            this.readMode = true;
            this.$http.request({
                url: 'http://192.168.1.5:5000/credential',
                method: "post",
                headers: {
                    "Authorization": "BEARER "+ this.$loginToken,
                    "Content-Type": "application/json"
                },
                data: {
                    id: this.cred.id,
                    type: parseInt(this.cred.type),
                    domain: this.cred.domain,
                    hint: this.cred.hint
                }
            })
            .then(resp => {
                if (resp.status == 200) {
                    console.log("Sucess!");
                }
            });
        }
    }
}
</script>
<style scoped>
* {
    font-size: 14px;
    font-weight: 700;
}

#credential-item {
    padding: 0 30px;
    display: flex;
    justify-content: space-between;
    align-items: center;

}
h4 {
    text-align: center;
}
input {
    margin: 10px;
    text-align: center;
    border: none;
    background-color: inherit;
    font-size: 14px;
}
.type {
    width: 150px;
}
.domain, .hint {
    width: 130px;
}
.edit, .submit {
    width: 40px;
}
</style>