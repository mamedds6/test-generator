<template>
<div>
    <h2>Question statement</h2> 
    <textarea v-model="question" placeholder="Write the question statement here..."></textarea>
    <h2>Alternatives: {{index}}</h2>
    <button v-on:click="plus">+</button>
    <button v-on:click="minus">-</button>
    <br>
    <template v-for="n in index">
        <h4>{{n}}</h4>
        <textarea v-model="alternatives[n-1]" placeholder="Write the first alternative here..."></textarea>
        <br>
    </template>

    <p>Correct index</p>
    <input v-model="correctIndex" placeholder="Ex: 1">
    <br>
    <br>
    <button v-on:click="add">Add</button>

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
                alternatives: ['',''],
                index: 2,
                correctIndex: ''
            }
        },
        methods:
        {
            plus: function ()
            {
                this.index += 1;
                this.alternatives.push('')
            },
            minus: function ()
            {
                if(this.index > 2)
                {
                    this.alternatives.pop()
                    this.index -= 1;
                }
            },
            add: function()
            {
                if(!isNaN(this.correctIndex) && this.correctIndex > 0 && this.correctIndex <= this.index)
                    alert("VALID")
                else
                    alert("NOT VALID")
            }
            
        }
    }
</script>