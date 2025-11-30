
function timeAgo(date) {
  const seconds = Math.floor((new Date() - new Date(date)) / 1000);

  const intervals = {
    year:   { lable: "năm", value: 365 * 24 * 60 * 60 },
    month:  { lable: "tháng", value: 30  * 24 * 60 * 60 },
    week:   { lable: "tuần", value: 7   * 24 * 60 * 60 },
    day:    { lable: "ngày", value: 24  * 60 * 60 },
    hour:   { lable: "giờ", value: 60  * 60 },
    minute: { lable: "phút", value: 60 },
    second: { lable: "giây", value:  1 }
  };

  for (const unit in intervals) {
    console.log(seconds)
    const count = Math.floor(seconds / intervals[unit].value);
    if (count >= 1) {
      return `${count} ${intervals[unit].lable} trước`;
    }
  }
  return "Bây giờ";
}


document.querySelectorAll(".cv-card__update-at").forEach(e => {
  let textChanged = timeAgo(e.innerText);
  e.innerText = textChanged;
})
