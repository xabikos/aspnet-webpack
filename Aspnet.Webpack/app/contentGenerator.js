import loremIpsum from 'lorem-ipsum';

const contetGenerator =  {
  getContent(count = 5, type = 'sentences') {
    return loremIpsum({
      count,
      unist: type
    });
  }
};

export default contetGenerator;