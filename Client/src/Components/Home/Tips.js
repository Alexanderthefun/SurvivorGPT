

export const getRandomTips = () => {
    const survivalTips = [
        { id: 1, tip: "Prioritize shelter to protect yourself from the elements." },
        { id: 2, tip: "Find a clean water source and stay hydrated." },
        { id: 3, tip: "Build a fire for warmth, cooking, and signaling for help." },
        { id: 4, tip: "Learn basic first aid skills to treat injuries." },
        { id: 5, tip: "Carry a signal mirror or whistle to help rescuers locate you." },
        { id: 6, tip: "Know how to navigate using a map and compass." },
        { id: 7, tip: "Wear layers of clothing to stay warm and dry." },
        { id: 8, tip: "Never eat plants or berries unless you're certain they're safe." },
        { id: 9, tip: "Be cautious around wildlife and avoid attracting predators." },
        { id: 10, tip: "Create and maintain a survival kit with essential tools and supplies." },
        { id: 11, tip: "Stay calm and focused to make rational decisions in emergency situations." },
        { id: 12, tip: "Learn how to build a simple snare or trap to catch small game." },
        { id: 13, tip: "Conserve energy by pacing yourself and taking breaks when needed." },
        { id: 14, tip: "Keep a positive attitude and maintain the will to survive." },
        { id: 15, tip: "Avoid traveling during the hottest or coldest parts of the day." },
        { id: 16, tip: "In the desert, stay in the shade and minimize water loss by breathing through your nose." },
        { id: 17, tip: "Learn how to tell direction using the sun or stars." },
        { id: 18, tip: "Collect rainwater, dew, or snow for hydration when water sources are scarce." },
        { id: 19, tip: "Always let someone know your travel plans and estimated return time." },
        { id: 20, tip: "In the event of a natural disaster, follow local authorities' advice and evacuate if necessary." },
      ];

    const randomIndex = Math.floor(Math.random() * survivalTips.length);
    return survivalTips[randomIndex].tip;
  }