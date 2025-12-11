
function timeAgo(date) {
    const parts = date.match(/(\d{1,2})\/(\d{1,2})\/(\d{4}) (.*)/);
    const day = parts[1];
    const month = parts[2];
    const year = parts[3];
    const time = parts[4];
    const correctDateString = `${month}/${day}/${year} ${time}`;
    const seconds = Math.floor((new Date() - new Date(correctDateString)) / 1000);

    console.log(new Date(date));
  const intervals = {
    year:   { label: "năm", value: 365 * 24 * 60 * 60 },
    month:  { label: "tháng", value: 30  * 24 * 60 * 60 },
    week:   { label: "tuần", value: 7   * 24 * 60 * 60 },
    day:    { label: "ngày", value: 24  * 60 * 60 },
    hour:   { label: "giờ", value: 60  * 60 },
    minute: { label: "phút", value: 60 },
    second: { label: "giây", value:  1 }
  };

  for (const unit in intervals) {
    const count = Math.floor(seconds / intervals[unit].value);
    if (count >= 1) {
      return `${count} ${intervals[unit].label} trước`;
    }
  }
  return "Bây giờ";
}


document.querySelectorAll(".cv-card__update-at").forEach(e => {
  let textChanged = timeAgo(e.innerText);
  e.innerText = textChanged;
})
