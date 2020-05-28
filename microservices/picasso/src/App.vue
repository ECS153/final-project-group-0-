<template>
  <div id="app">
    <div id="nav">
      <router-link to="/">Home</router-link>
      <router-link to="/credential">Credentials </router-link>
      <router-link to="/account">Account</router-link>
    </div>
    <router-view />
  </div>
</template>

<style>
@import url("https://fonts.googleapis.com/css2?family=Noto+Sans:wght@400;700&display=swap");

#app,
body,
html {
  height: 100%;
}
#app {
  font-family: "Noto Sans", sans-serif;
  display: flex;
  flex-flow: column;
  justify-content: flex-start;
  align-items: center;
}
#nav {
  width: 100%;
  height: 30px;
  padding: 15px 30px 0 15px;
  display: flex;
  flex-flow: row;
  justify-content: flex-end;
  align-items: flex-end;
}
#nav a {
  padding: 0 5px 0 5px;
  font-weight: bold;
  color: #2c3e50;
  text-decoration: none;
}
#nav a.router-link-exact-active {
  color: #f50057;
}
</style>
