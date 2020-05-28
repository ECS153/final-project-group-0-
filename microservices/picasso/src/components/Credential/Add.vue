<template>
  <div class="credential-add">
    <transition name="slide-fade" mode="out-in">
      <div v-if="wasClicked == false" :key="1">
        <button @click="wasClicked=!wasClicked" class="button primary">New</button>
      </div>
      <div v-else :key="2" id="cred-form" class="card shadow-l">
        <div id="flex-row">
          <h2>Hint</h2>
          <input v-model="hint" type="text" />
        </div>
        <div id="flex-row">
          <h2>Value</h2>
          <input type="password" v-model="val" />
        </div>
        <div id="flex-row">
          <h2>Domain</h2>
          <input v-model="domain" placeholder="Leave blank for all sites" type="text">
        </div>
        <select v-model="type">
          <option disabled value="">Please select one</option>
          <option value="0">Password</option>
          <option value="1">Credit Card</option>
          <option value="2">Username</option>
          <option value="3">Email</option>
        </select>
        <div id="flex-row">
          <button @click="wasClicked=!wasClicked" class="button secondary">Cancel</button>
          <button @click="submit" class="button primary">Submit</button>
        </div>
      </div>
    </transition>
  </div>
</template>
<script>
export default {
  name: "CredentialAdd",
  data() {
    return {
      type: 0,
      hint: "",
      val: "",
      domain: "",
      wasClicked: false
    };
  },
  methods: {
    submit: function() {
      this.$http
        .request({
          url: "http://192.168.1.5:5000/credential/new",
          method: "post",
          headers: this.$apiHeaders,
          data: {
            Type: parseInt(this.type),
            Hint: this.hint,
            Value: this.val,
            Domain: this.domain
          }
        })
        .then(resp => {
          if (resp.status == 200) {
            this.wasClicked = false;
          }
        })
        .catch ((err) => {
          if (err.response.status == 401) {
            this.$toast.error("Unauthorized");
          } else {
            this.$toast.error(err.response.data.title);
          }
      });
    }
  }
};
</script>

<style scoped>
.credential-add {
  width: 100%;
  padding: 0;
}
.button-primary {
  align-self: flex-end;
}
#flex-row {
  flex-grow: 1;
  display: flex;
  flex-flow: row;
  justify-content: space-around;
  align-items: center;
}
#cred-form {
  padding: 30px;
  height: 300px;
  display: flex;
  flex-flow: column;
  align-items: space-around;
  justify-content: stretch;
}

h2 {
  font-size: 20px;
  color: #29434e;
  padding: 10px;
  min-width: 80px;
}
input {
  color: #f50057;
  font-size: 20px;
  height: 28px;
}

.slide-f-enter-active {
  transition: all 0.2s ease-out;
}
.slide-f-leave-active {
  transition: all 0.2s ease-in;
}
.slide-f-enter {
  transform: translateY(15px);
  opacity: 0;
}
.slide-f-leave-to
  /* .slide-fade-leave-active below version 2.1.8 */ {
  transform: translateY(-15px);
  opacity: 0;
}

.primary {
  background-color: #f50057;
}
.secondary {
  background-color: #29434e;
}
</style>
