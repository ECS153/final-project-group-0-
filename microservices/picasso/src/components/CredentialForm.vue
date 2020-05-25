<template>
    <div class="box-shadow-3">
       <h2><span class="title">Domain: </span>{{swapProps.domain}}</h2>
       <h2><span class="title">Field-Id: </span>{{swapProps.fieldId}}</h2>
       <select v-model="selectedCred" id="creds">
            <option v-for="cred in creds" v-bind:value="cred" :key="cred.id"> {{cred.hint}} </option>
        </select>
        <button @click="submitSwap" class="box-shadow-2">Submit</button>   
    </div>
</template>

<script>
import axios from 'axios'

export default {
    name: 'CredentialForm',
    props: [ 'token', 'swapProps'],
    data() {
        return {
            selectedCred: {hint:'', id:'' },
            creds: String
        }
    },
    methods: {
        submitSwap: function() {
            axios.request({
                url: 'http://192.168.1.5:5000/pi',
                method: 'post',
                headers: {
                    'Authorization': 'BEARER ' + this.token,
                    'Content-Type': 'application/json'
                },
                data: {
                    'SwapId': this.swapProps.id,
                    'CredentialId': this.selectedCred.id
                }
            }).then(resp => {
                if (resp.status == 200) {
                    this.$emit('submit')
                }
            })
        }
    },
    // right after creation, get possible credentials that can be used
    created: function() {
        axios.request({
            url: 'http://192.168.1.5:5000/credential',
            method: 'get',
            headers: {
                'Authorization': 'BEARER ' + this.token,
                'Content-Type': 'application/json'
            },
            params: {
                'domain': this.swapProps.domain,
                'type': this.swapProps.type
            }
        })
        .then(resp => {
            if (resp.status == 200) {
                this.creds = resp.data
        }})
    }
}
</script>

<style scoped>
@import url('https://fonts.googleapis.com/css2?family=Noto+Sans:wght@400;700&display=swap');

* {
    font-family: 'Noto Sans', sans-serif;
}
div {
    width: 400px;
    height: 300px;
    border-radius: 10px;
    padding: 15px;
    background-color: #f5f5f5;

    display: flex;
    flex-flow: column;
    justify-content: space-around;
    align-items: stretch;
}


.title {
    font-size: 36px;
    font-weight: 700;
    color: #29434e;
}
h2 {
    font-size: 24px;
    color: #f50057;
    padding: 10px;
}
select {
    outline: none;
    box-shadow: none;
    border: none;
    border-bottom: solid 1px #29434e;
    background-color: #f5f5f5;
}
option {
    padding: 20px;
    font-size: 25px;
    color: #29434e;
}
button {
    align-self: center;
    width: 100px;
    height: 60px;
    outline: none;
    border:none;
    border-radius: 10px;
    border: solid 10px #f50057;
    font-size: 18px;
    color: white;

    background: #f50057;
    background-size: 50%;
    background-repeat: no-repeat;
    background-position: center;
}

.box-shadow-3 {
	box-shadow: 0 14px 28px rgba(0, 0, 0, 0.25), 0 10px 10px rgba(0, 0, 0, 0.22);
	margin: 15px;
}
.box-shadow-2 {
	box-shadow: 0 10px 20px rgba(0,0,0,0.19), 0 6px 6px rgba(0,0,0,0.23);
	margin: 10px;
}
</style>
