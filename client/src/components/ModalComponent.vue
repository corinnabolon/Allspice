<template>
  <div class="modal fade" :id="modalId" tabindex="-1" :aria-labelledby="modalId + 'Label'" aria-hidden="true">
    <div class="modal-dialog" :class="modalSize">
      <div class="modal-content">
        <div class="modal-header mb-0 pb-0 d-flex align-items-center">
          <p class="mb-0" :id="modalId + 'Label'">

            <slot name="modalTitle"></slot>

          </p>
          <button type="button" class="btn-close mb-0" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
        <div class="modal-body">

          <slot name="modalBody"></slot>

        </div>
      </div>
    </div>
  </div>
</template>


<script>
import { recipesService } from "../services/RecipesService.js";



export default {
  props: {
    modalId: { type: String, required: true },
    modalSize: { type: String, default: '' }
  },
  setup() {
    return {
    }
  }
}
</script>


<style lang="scss" scoped></style>