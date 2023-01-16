using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 华泽_会计基础模拟考试
{
    public partial class 功能测试 : Form
    {
        public 功能测试()
        {
            InitializeComponent();
        }

        private void 功能测试_Load(object sender, EventArgs e)
        {
            cmb借贷.Items.Add("借");
            cmb借贷.Items.Add("贷");

            cmb借贷科目.Items.Add("现金");
            cmb借贷科目.Items.Add("银行存款");
            cmb借贷科目.Items.Add("其他货币资金");
            cmb借贷科目.Items.Add("交易性金融资产");
            cmb借贷科目.Items.Add("交易性金融资产－成本");
            cmb借贷科目.Items.Add("交易性金融资产－公允价值变动");

            cmb借贷科目.Items.Add("短期投资");
            cmb借贷科目.Items.Add("短期投资跌价准备");
            cmb借贷科目.Items.Add("应收票据");
            cmb借贷科目.Items.Add("应收股利");
            cmb借贷科目.Items.Add("应收账款");
            cmb借贷科目.Items.Add("其他应收款");
            cmb借贷科目.Items.Add("坏账准备");
            cmb借贷科目.Items.Add("在途物资");
            cmb借贷科目.Items.Add("原材料");
            cmb借贷科目.Items.Add("材料采购");
            cmb借贷科目.Items.Add("材料成本差异");
            cmb借贷科目.Items.Add("低值易耗品");
            cmb借贷科目.Items.Add("库存商品");
            cmb借贷科目.Items.Add("商品进销差价");
            cmb借贷科目.Items.Add("委托加工物资");
            cmb借贷科目.Items.Add("委托代销商品");
            cmb借贷科目.Items.Add("存货跌价准备");
            cmb借贷科目.Items.Add("待摊费用");
            cmb借贷科目.Items.Add("待处理财产损溢");
            cmb借贷科目.Items.Add("长期股权投资");
            cmb借贷科目.Items.Add("长期债权投资");
            cmb借贷科目.Items.Add("固定资产");
            cmb借贷科目.Items.Add("累计折旧");
            cmb借贷科目.Items.Add("工程物资");
            cmb借贷科目.Items.Add("在建工程");
            cmb借贷科目.Items.Add("固定资产清理");
            cmb借贷科目.Items.Add("无形资产");
            cmb借贷科目.Items.Add("长期待摊费用");
            cmb借贷科目.Items.Add("短期借款");
            cmb借贷科目.Items.Add("应付票据");
            cmb借贷科目.Items.Add("应付账款");
            cmb借贷科目.Items.Add("应付工资");
            cmb借贷科目.Items.Add("应付福利费");
            cmb借贷科目.Items.Add("应付利润");
            cmb借贷科目.Items.Add("应交税金");
            cmb借贷科目.Items.Add("应交税金－应交增值税");
            cmb借贷科目.Items.Add("应交税金－进项税额");
            cmb借贷科目.Items.Add("应交税金－已交税金");
            cmb借贷科目.Items.Add("应交税金－减免税款");
            cmb借贷科目.Items.Add("应交税金－出口抵减内销产品应纳税额");
            cmb借贷科目.Items.Add("应交税金－转出未交增值税");
            cmb借贷科目.Items.Add("应交税金－销项税额");
            cmb借贷科目.Items.Add("应交税金－出口退税");
            cmb借贷科目.Items.Add("应交税金－进项税额转出");
            cmb借贷科目.Items.Add("应交税金－转出多交增值税");
            cmb借贷科目.Items.Add("应交税金－未交增值税");
            cmb借贷科目.Items.Add("应交税金－应交营业税");
            cmb借贷科目.Items.Add("应交税金－应交消费税");
            cmb借贷科目.Items.Add("应交税金－应交资源税");
            cmb借贷科目.Items.Add("应交税金－应交所得税");
            cmb借贷科目.Items.Add("应交税金－应交土地增值税");
            cmb借贷科目.Items.Add("应交税金－应交城市维护建设税");
            cmb借贷科目.Items.Add("应交税金－应交房产税");
            cmb借贷科目.Items.Add("应交税金－应交土地使用税");
            cmb借贷科目.Items.Add("应交税金－应交车船使用税");
            cmb借贷科目.Items.Add("应交税金－应交个人所得税");

            cmb借贷科目.Items.Add("其他应交款");
            cmb借贷科目.Items.Add("其他应付款");
            cmb借贷科目.Items.Add("预提费用");
            cmb借贷科目.Items.Add("待转资产价值");
            cmb借贷科目.Items.Add("长期借款");
            cmb借贷科目.Items.Add("长期应付款");
            cmb借贷科目.Items.Add("实收资本");
            cmb借贷科目.Items.Add("资本公积");
            cmb借贷科目.Items.Add("盈余公积");
            cmb借贷科目.Items.Add("本年利润");

            cmb借贷科目.Items.Add("利润分配");
            cmb借贷科目.Items.Add("利润分配－其他转入");
            cmb借贷科目.Items.Add("利润分配－提取法定盈余公积");
            cmb借贷科目.Items.Add("利润分配－提取法定公益金");
            cmb借贷科目.Items.Add("利润分配－提取任意盈余公积");
            cmb借贷科目.Items.Add("利润分配－应付利润");
            cmb借贷科目.Items.Add("利润分配－转作资本的利润");
            cmb借贷科目.Items.Add("利润分配－分配利润");

            cmb借贷科目.Items.Add("生产成本");
            cmb借贷科目.Items.Add("劳务成本");
            cmb借贷科目.Items.Add("制造费用");
            cmb借贷科目.Items.Add("主营业务收入");
            cmb借贷科目.Items.Add("其他业务收入");
            cmb借贷科目.Items.Add("公允价值变动损益");
            cmb借贷科目.Items.Add("投资收益");
            cmb借贷科目.Items.Add("资产减值损失");
            cmb借贷科目.Items.Add("营业外收入");
            cmb借贷科目.Items.Add("主营业务成本");
            cmb借贷科目.Items.Add("营业税金及附加");
            cmb借贷科目.Items.Add("其他业务支出");
            cmb借贷科目.Items.Add("营业费用");
            cmb借贷科目.Items.Add("管理费用");
            cmb借贷科目.Items.Add("财务费用");
            cmb借贷科目.Items.Add("营业外支出");
            cmb借贷科目.Items.Add("所得税");
        }
        private void button3_Click(object sender, EventArgs e)
        {
            string abc = "This is a book.and @136";
            int d = abc.IndexOf("@");
            this.Text = d.ToString();
            int mye=abc.Length -d-1;
            textBox1.Text = abc.Substring(d+1,mye);
        }
    }
}
