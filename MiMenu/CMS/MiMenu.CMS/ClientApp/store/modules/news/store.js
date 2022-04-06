
  const store= {
   
    state: {
        articles: [
            { title: 'title 1', author: 'admin' },
            { title: 'title 2', author: 'pet' },
            { title: 'title 3', author: 'ces' },
            { title: 'title 4', author: 'timi' }
        ]
    },

    getters: {
        articles: state => state.articles,
        // Here we will create a getter
    },

    mutations: {
        // Here we will create Jenny
    },

    actions: {
        // Here we will create Larry
    }
};
export default store;

