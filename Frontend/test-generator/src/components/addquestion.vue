<template>
<div>
    <h2>Question statement</h2> 
    <textarea v-model="question" placeholder="Write the question statement here..."></textarea>
    <h2>Alternatives</h2>  
    <input type="radio" id="one" value="One" v-model="picked">
    <textarea v-model="x" placeholder="Write the first alternative here..."></textarea>
    <br>
    <input type="radio" id="two" value="Two" v-model="picked">
    <textarea v-model="d" placeholder="Write the first alternative here..."></textarea>





</div>
</template>

<script>
    export default {
        name: 'AddQuestions',
        inputs:
        {
            name: ''
        },
        components: {
            
        },
        data() {
            return {
                message: '',
                index: 0,
                picked: ''
            }
        },
        methods:
        {
            add(index) {
                this.inputs.push({ name: '' });
                index+=1;
                index-=1;
            },
            remove(index) {
                this.inputs.splice(index, 1);
            }
        }
    }
</script>