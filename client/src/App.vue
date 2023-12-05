<template>
  <header>
    <Navbar />
  </header>
  <main>
    <router-view />
  </main>
</template>

<script>
import { computed } from 'vue'
import { AppState } from './AppState'
import Navbar from './components/Navbar.vue'

export default {
  setup() {
    return {
      appState: computed(() => AppState)
    }
  },
  components: { Navbar }
}
</script>
<style lang="scss">
@import "./assets/scss/main.scss";

:root {
  --main-height: calc(100vh - 32px - 64px);
  --theme-green: #527853;
  --theme-pink: #F9E8D9;
  --theme-peach: #F7B787;
  --theme-orange: #EE7214;
}

main {
  background-color: var(--theme-pink);
}

footer {
  display: grid;
  place-content: center;
  height: 32px;
}

.bg-theme-green {
  background-color: var(--theme-green);
}

.bg-theme-pink {
  background-color: var(--theme-pink);
}

.bg-theme-peach {
  background-color: var(--theme-peach);
}

.bg-theme-orange {
  background-color: var(--theme-orange);
}

.text-theme-green {
  color: var(--theme-green);
}

.text-theme-pink {
  color: var(--theme-pink);
}

.text-theme-peach {
  color: var(--theme-peach);
}

.text-theme-orange {
  color: var(--theme-orange);
}

.btn-theme-green {
  background-color: var(--theme-green);
  border: 1px solid var(--theme-peach);
  color: var(--theme-pink);
}

.btn-theme-green:hover {
  background-color: var(--theme-orange);
  border: 1px solid var(--theme-peach);
  color: var(--theme-pink);
}

//The below theme not working?
.btn-theme-green:disabled {
  background-color: var(--theme-pink);
  border: 1px solid var(--theme-peach);
  color: var(--theme-green);
}

.btn-theme-orange {
  background-color: var(--theme-orange);
  border: 1px solid var(--theme-peach);
  color: var(--theme-green);
}

.btn-theme-orange:hover {
  background-color: firebrick;
  border: 1px solid var(--theme-orange);
  color: var(--theme-pink);
}

.serif-font {
  font-family: 'DM Serif Display', serif;
}
</style>
