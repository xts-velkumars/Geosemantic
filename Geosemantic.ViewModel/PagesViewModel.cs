namespace Geosemantic.ViewModel
{
    public class PagesViewModel
    {
        public long Id { get; set; }

        public string Name { set; get; }

        public string ShortName { set; get; }

        public long Sequence { set; get; }

        public string Url { set; get; }

        public long ParentId { set; get; }

        public string Icon { set; get; }

        public string Label { set; get; }

        public string HasBadge { set; get; }

        public string BadgeText { set; get; }
    }
}
