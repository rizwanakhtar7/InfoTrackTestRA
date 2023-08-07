import React, { useRef, useEffect } from 'react';

function CountAnimation({ end }) {
  const countRef = useRef(null);

  useEffect(() => {
    const element = countRef.current;
    const start = 0;
    const duration = 500;
    const range = end - start;
    const increment = range / duration * 10;

    let current = start;
    const interval = setInterval(() => {
      current += increment;
      element.textContent = Math.floor(current);
      if (current >= end) {
        clearInterval(interval);
      }
    }, 10);

    return () => clearInterval(interval);
  }, [end]);

  return (
   <div>
      <span ref={countRef}>0</span>
    </div>
  );
}

export default CountAnimation;